using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private readonly TicTacToeDbContext context;
        private readonly IGameResultValidator gameValidator;

        public GameService(TicTacToeDbContext context, IGameResultValidator gameValidator)
        {
            this.context = context;
            this.gameValidator = gameValidator;
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

            return game.ToGameStatusOutput();
        }

        /// <inheritdoc />
        public GameStatusOutput Join(Guid gameId, Guid opponentUserId)
        {
            Random r = new Random();
            var game = this.context.Games
                .Include(g => g.CreatorUser)
                .FirstOrDefault(g => g.GameId == gameId);

            game.OpponentUserId = opponentUserId;
            var turn = r.Next(0, 2);
            if (turn == 0)
            {
                game.State = GameState.CreatorTurn;
            }
            else
            {
                game.State = GameState.OpponentTurn;
            }

            context.SaveChanges();

            return game.ToGameStatusOutput();
        }

        /// <inheritdoc />
        public GameStatusOutput Status(Guid gameId, Guid userId)
        {
            var game = this.context.Games.Include(x => x.CreatorUser)
                                         .Include(x => x.OpponentUser)
                                         .AsNoTracking()
                                         .FirstOrDefault(g => g.GameId == gameId);

            return game.ToGameStatusOutput();
        }

        /// <inheritdoc />
        public GameStatusOutput Play(Guid gameId, Guid userId, int row, int col)
        {
            var game = this.context.Games.Include(x => x.CreatorUser)
                .Include(x => x.OpponentUser)
                .FirstOrDefault(g => g.GameId == gameId);

            var user = this.context.Users.Include(u => u.Scores).AsNoTracking().FirstOrDefault(u => u.UserId == userId);

            var boardArray = game.Board.ToCharArray();
            var position = 3 * row + col;
            char playerChar;

            if (game.State == GameState.CreatorTurn)
            {
                playerChar = 'X';
            }
            else
            {
                playerChar = 'O';
            }

            boardArray[position] = playerChar;
            string boardInsertedPosition = string.Join(string.Empty, boardArray);
            var gameResult = gameValidator.GetGameResult(boardInsertedPosition);

            game.Board = boardInsertedPosition;

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
            return game.ToGameStatusOutput();
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