using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IGameResultValidator
    {
        /// <summary>
        /// Gets the game result by provided coordinates.
        /// </summary>
        /// <param name="board">The game's board.</param>
        /// <param name="row">The selected row.</param>
        /// <param name="col">The selected col.</param>
        /// <returns>The game result indicating whether the result is not finished, won by X or O, draw, etc.</returns>
        GameResult GetGameResult(string board, int row, int col);
    }
}