﻿using System;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Creates a new game session by given name.
        /// </summary>
        /// <param name="name">The game's name.</param>
        /// <param name="creatorUserId">The creator user's identifier.</param>
        /// <returns>The newly created game's identifier.</returns>
        Guid Create(string name, Guid creatorUserId);

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