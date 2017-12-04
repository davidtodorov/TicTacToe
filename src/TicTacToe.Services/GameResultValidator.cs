using System;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services
{
    public class GameResultValidator : IGameResultValidator
    {
        /// <inheritdoc />
        public GameResult GetGameResult(string board)
        {
            throw new NotImplementedException();
        }
    }
}