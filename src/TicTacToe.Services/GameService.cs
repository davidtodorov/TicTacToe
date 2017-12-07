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

        public GameService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public ICollection<AvailableGameInfoOutput> GetAvailableGames(Guid opponentUserId)
        {
            var games = this.context.Games.Where(x => x.State == GameState.WaitingForASecondPlayer && x.CreatorUserId != opponentUserId)
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
            var result = game.ToGameStatusOutput();
            return result;
        }

        /// <inheritdoc />
        public GameStatusOutput Join(Guid gameId, Guid opponentUserId)
        {
            var game = this.context.Games
                .Include(g => g.CreatorUser)
                .FirstOrDefault(g => g.GameId == gameId);
            game.OpponentUserId = opponentUserId;
            game.State = GameState.CreatorTurn;
            context.SaveChanges();
            var result = game.ToGameStatusOutput();
            return result;
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
            var game = this.context.Games.FirstOrDefault(g => g.GameId == gameId);           
            var player1 = this.context.Users.FirstOrDefault(u => u.UserId == userId);
            throw new Exception();
        }
    }
}