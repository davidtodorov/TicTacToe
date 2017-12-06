using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Join(Guid gameId, Guid opponentUserId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Status(Guid gameId, Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Play(Guid gameId, Guid userId, int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}