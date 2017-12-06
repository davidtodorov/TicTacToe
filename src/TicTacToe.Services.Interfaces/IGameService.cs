using System;
using System.Collections.Generic;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Gets all available games waiting for an opponent.
        /// </summary>
        /// <param name="opponentUserId">The opponent user's identifier searching for a game.</param>
        /// <returns>A collection of all available games.</returns>
        ICollection<AvailableGameInfoOutput> GetAvailableGames(Guid opponentUserId);

        /// <summary>
        /// Creates a new game session by given name.
        /// </summary>
        /// <param name="input">The game's input information.</param>
        /// <param name="creatorUserId">The creator user's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Create(GameCreationInput input, Guid creatorUserId);

        /// <summary>
        /// Joins to a game session by given game's identifier.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="opponentUserId">The opponent user's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Join(Guid gameId, Guid opponentUserId);

        /// <summary>
        /// Gets a status information about a game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Status(Guid gameId);

        /// <summary>
        /// Plays a turn for a given game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="row">The specified row.</param>
        /// <param name="col">The specified column.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Play(Guid gameId, int row, int col);
    }
}