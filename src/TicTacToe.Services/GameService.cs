using System;
using TicTacToe.Data;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;

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
        public Guid Create(string name, Guid creatorUserId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Join(Guid gameId, Guid opponentUserId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Status(Guid gameId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public GameStatusOutput Play(Guid gameId, int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}