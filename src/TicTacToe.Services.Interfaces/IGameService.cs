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
        /// <param name="userId">The opponent user's identifier searching for a game.</param>
        /// <returns>A collection of all available games.</returns>
        ICollection<AvailableGameInfoOutput> GetAvailableGames(Guid userId);

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
        /// <param name="userId">The opponent user's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Join(Guid gameId, Guid userId);

        /// <summary>
        /// Gets a status information about a game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="userId">The user's identifier requesting the status.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Status(Guid gameId, Guid userId);

        /// <summary>
        /// Plays a turn for a given game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="userId">The user's identifier playing a turn.</param>
        /// <param name="row">The specified row.</param>
        /// <param name="col">The specified column.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusOutput Play(Guid gameId, Guid userId, int row, int col);
    }
}