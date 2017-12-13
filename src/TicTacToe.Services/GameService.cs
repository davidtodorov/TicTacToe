using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private readonly TicTacToeDbContext context;
        private readonly IGameResultValidator gameValidator;
        private readonly Random randomGenerator;

        public GameService(TicTacToeDbContext context, IGameResultValidator gameValidator)
        {
            this.context = context;
            this.gameValidator = gameValidator;
            this.randomGenerator = new Random();
        }

        /// <inheritdoc />
        public ICollection<AvailableGameInfoOutput> GetAvailableGames(Guid userId)
        {
            var games = this.context.Games.Where(x => x.State == GameState.WaitingForASecondPlayer && x.CreatorUserId != userId)
                                          .OrderByDescending(x => x.CreationDate)
                                          .Select(GameMappings.ToAvailableGameInfoOutput)
                                          .ToList();
            return games;
        }

        /// <inheritdoc />
        public GameStatusOutput Create(GameCreationInput input, Guid creatorUserId)
        {
            var game = new Game()
            {
                Name = input.Name,
                Visibility = input.Visibility,
                HashedPassword = input.Password,
                CreatorUserId = creatorUserId,
                State = GameState.WaitingForASecondPlayer
            };

            context.Games.Add(game);
            context.SaveChanges();

            return game.ToGameStatus();
        }

        /// <inheritdoc />
        public GameStatusOutput Join(Guid gameId, Guid userId)
        {
            //// Check if all games are available for second player, filter game, similar to getAvailableGames
            //// Opponent user (userId)

            var game = this.context.Games
                .Include(g => g.CreatorUser)
                .Include(g => g.OpponentUser)
                .FirstOrDefault(g => g.GameId == gameId);

            //// Check for existing game
            var randNum = randomGenerator.Next(0, 2);
            game.State = randNum == 0 ? GameState.CreatorTurn : GameState.OpponentTurn;
            game.OpponentUserId = userId;

            context.SaveChanges();

            return game.ToGameStatus();
        }

        /// <inheritdoc />
        public GameStatusOutput Status(Guid gameId, Guid userId)
        {
            var game = this.context.Games.Select(GameMappings.ToGameStatusOutput)
                                         .FirstOrDefault(g => g.Id == gameId && (userId == g.CreatorUserId || userId == g.OpponentUserId));

            if (game == null)
            {
                throw new NotFoundException($"The game with identifier: '{gameId}' was not found.");
            }
            
            return game;
        }

        /// <inheritdoc />
        public GameStatusOutput Play(Guid gameId, Guid userId, int row, int col)
        {
            // Add check for userId to be creator or opponent
            // If exists
            // check the state if it's creator or opponent turn
            var game = this.context.Games.Include(x => x.CreatorUser)
                .Include(x => x.OpponentUser)
                .FirstOrDefault(g => g.GameId == gameId);

            char playerChar;

            if (game.State == GameState.CreatorTurn)
            {
                // Check if its creator, throw validation exception, not your turn
                playerChar = 'X';
            }
            else
            {
                // Check if its opponent
                playerChar = 'O';
            }
            
            var boardArray = game.Board.ToCharArray();

            // Check if position is valid, create method in gameResultValidator
            // Check if the position is taken, =! '-'
            var position = 3 * row + col;

            boardArray[position] = playerChar;
            string boardInsertedPosition = string.Join(string.Empty, boardArray);
            var gameResult = gameValidator.GetGameResult(boardInsertedPosition);

            game.Board = boardInsertedPosition;

            // Create private method for if/else
            if (gameResult == GameResult.NotFinished)
            {
                if (game.State == GameState.CreatorTurn)
                {
                    game.State = GameState.OpponentTurn;
                }
                else if (game.State == GameState.OpponentTurn)
                {
                    game.State = GameState.CreatorTurn;
                }
            }
            else if (gameResult == GameResult.WonByX)
            {
                game.State = GameState.CreatorVictory;
                CreateScore(game, game.CreatorUserId, ScoreStatus.Win);
                CreateScore(game, game.OpponentUserId.Value, ScoreStatus.Loss);
            }
            else if (gameResult == GameResult.WonByO)
            {
                game.State = GameState.OpponentVictory;
                CreateScore(game, game.CreatorUserId, ScoreStatus.Loss);
                CreateScore(game, game.OpponentUserId.Value, ScoreStatus.Win);
            }
            else if (gameResult == GameResult.Draw)
            {
                game.State = GameState.Draw;
                CreateScore(game, game.CreatorUserId, ScoreStatus.Draw);
                CreateScore(game, game.OpponentUserId.Value, ScoreStatus.Draw);
            }
            
            context.SaveChanges();
            return game.ToGameStatus();
        }
        
        private void CreateScore(Game game, Guid userId, ScoreStatus status)
        {
            var score = new Score()
            {
                GameId = game.GameId,
                UserId = userId,
                Status = status
            };

            context.Scores.Add(score);
        }
    }
}