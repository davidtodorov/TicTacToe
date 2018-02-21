using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public ICollection<AvailableGameInfoOutput> GetAvailableGames(string userId)
        {
            Expression<Func<Game, bool>> expression = x => x.State == GameState.WaitingForASecondPlayer 
                                                           && x.CreatorUserId != userId 
                                                           && (x.Visibility == VisibilityType.Public || x.Visibility == VisibilityType.Protected);
            return GetGames(expression, userId); 
        }

        /// <inheritdoc />
        public ICollection<AvailableGameInfoOutput> GetUserGamesInProgress(string userId)
        {
            Expression<Func<Game, bool>> expression = x => (x.State == GameState.WaitingForASecondPlayer || x.State == GameState.CreatorTurn || x.State == GameState.OpponentTurn) && x.CreatorUserId == userId;
            return GetGames(expression, userId);
        }

        public ICollection<AvailableGameInfoOutput> GetUserJoinedGames(string userId)
        {
            Expression<Func<Game, bool>> expression = x => (x.State == GameState.WaitingForASecondPlayer || x.State == GameState.CreatorTurn || x.State == GameState.OpponentTurn) && x.OpponentUserId == userId;
            return GetGames(expression, userId);
        }

        /// <inheritdoc />
        public GameStatusOutput Create(GameCreationInput input, string creatorUserId)
        {
            if (string.IsNullOrWhiteSpace(creatorUserId))
            {
                throw new ValidationException("UserId cannot be null");
            }

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
        public GameStatusOutput Join(GameJoinInput input, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException("UserId cannot be null");
            }

            var game = this.context.Games
                .Where(x => x.State == GameState.WaitingForASecondPlayer && x.CreatorUserId != userId)
                .Include(g => g.CreatorUser)
                .Include(g => g.OpponentUser)
                .FirstOrDefault(g => g.GameId == input.GameId);

            if (game == null)
            {
                throw new NotFoundException($"The game cannot be found.");
            }

            ValidateGamePassword(game.Visibility, input.Password, game.HashedPassword);

            var randNum = randomGenerator.Next(0, 2);
            game.State = randNum == 0 ? GameState.CreatorTurn : GameState.OpponentTurn;
            game.OpponentUserId = userId;

            context.SaveChanges();

            return game.ToGameStatus();
        }

        /// <inheritdoc />
        public GameStatusOutput Status(Guid gameId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException("UserId cannot be null");
            }

            var game = this.context.Games.Select(GameMappings.ToGameStatusOutput)
                                         .FirstOrDefault(g => g.Id == gameId && (userId == g.CreatorUserId || userId == g.OpponentUserId));

            if (game == null)
            {
                throw new NotFoundException($"The game with identifier: '{gameId}' was not found.");
            }
            
            return game;
        }

        /// <inheritdoc />
        public GameStatusOutput Play(Guid gameId, string userId, int row, int col)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException("UserId cannot be null");
            }

            var game = this.context.Games
                .Include(g => g.CreatorUser)
                .Include(x => x.OpponentUser)
                .Where(g => g.CreatorUserId == userId || g.OpponentUserId == userId)
                .Where(g => g.State == GameState.CreatorTurn || g.State == GameState.OpponentTurn)
                .FirstOrDefault(g => g.GameId == gameId);

            if (game == null)
            {
                throw new NotFoundException($"The game with identifier: '{gameId}' was not found.");
            }
            
            if ((game.State == GameState.CreatorTurn && game.CreatorUserId != userId) || (game.State == GameState.OpponentTurn && game.OpponentUserId != userId))
            {
                throw new ValidationException($"It's not your turn");
            }

            char playerChar = game.State == GameState.CreatorTurn ? 'X' : 'O';

            var boardArray = game.Board.ToCharArray();

            if (!gameValidator.IsValidPosition(row))
            {
                throw new InvalidPositionException($"The input row: {row} is invalid");
            }

            if (!gameValidator.IsValidPosition(col))
            {
                throw new InvalidPositionException($"The input col: {col} is invalid");
            }

            // Check if position is valid, create method in gameResultValidator
            // Check if the position is taken, =! '-'
            var position = 3 * row + col;
  
            var isTaken = gameValidator.IsPositionTaken(game.Board, position);
            if (isTaken)
            {
                throw new ValidationException($"The position is already taken!");
            }

            boardArray[position] = playerChar;
            string boardInsertedPosition = string.Join(string.Empty, boardArray);
            var gameResult = gameValidator.GetGameResult(boardInsertedPosition);

            game.Board = boardInsertedPosition;
            
            this.CheckGameResult(gameResult, game);
            
            context.SaveChanges();
            return game.ToGameStatus();
        }

        public IList<GameScoresInfoOutput> GetScores()
        {
            var users = this.context.Users.Include(u => u.Scores).Select(GameMappings.ToGameScoresOutput).ToList();

            //// Var scores = this.context.Scores.Where(s => s.Status == ScoreStatus.Draw);
            return users;
        }

        public void ValidateGamePassword(VisibilityType visibility, string password, string gamePassword)
        {
            if (visibility == VisibilityType.Protected && password != gamePassword)
            {
                throw new ValidationException("Input password doesn't match");
            }
        }
        
        public void CheckGameResult(GameResult gameResult, Game game)
        {
            if (gameResult == GameResult.NotFinished)
            {
                game.State = game.State == GameState.CreatorTurn ? GameState.OpponentTurn : GameState.CreatorTurn;
            }
            else if (gameResult == GameResult.WonByX)
            {
                game.State = GameState.CreatorVictory;
                this.CreateScore(game, game.CreatorUserId, ScoreStatus.Win);
                this.CreateScore(game, game.OpponentUserId, ScoreStatus.Loss);
            }
            else if (gameResult == GameResult.WonByO)
            {
                game.State = GameState.OpponentVictory;
                this.CreateScore(game, game.CreatorUserId, ScoreStatus.Loss);
                this.CreateScore(game, game.OpponentUserId, ScoreStatus.Win);
            }
            else if (gameResult == GameResult.Draw)
            {
                game.State = GameState.Draw;
                this.CreateScore(game, game.CreatorUserId, ScoreStatus.Draw);
                this.CreateScore(game, game.OpponentUserId, ScoreStatus.Draw);
            }
        }

        private void CreateScore(Game game, string userId, ScoreStatus status)
        {
            var score = new Score()
            {
                GameId = game.GameId,
                UserId = userId,
                Status = status
            };

            context.Scores.Add(score);
        }

        private ICollection<AvailableGameInfoOutput> GetGames(Expression<Func<Game, bool>> expression, string userId)
        {
           if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException("UserId cannot be null");
            }

            return this.context.Games
                .Where(expression)
                .OrderByDescending(x => x.CreationDate)
                .Select(GameMappings.ToAvailableGameInfoOutput)
                .ToList();
        }
    }
}