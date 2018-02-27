using System.Collections.Generic;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IScoreService
    {
        /// <summary>
        /// Gets top 10 scores.
        /// </summary>
        /// <returns>A collection of all top 10 scores.</returns>
        IList<GameScoresInfoOutput> GetScores();
    }
}
