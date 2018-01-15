using System.Collections.Generic;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A collection with users from the database.</returns>
        ICollection<UserInfoOutput> AllUsers();

        /// <summary>
        /// Registers a user by the given parameters.
        /// </summary>
        /// <param name="input">The user's input data.</param>
        /// <returns>The created user's data.</returns>
        UserInfoOutput Register(UserRegistrationInput input);
    }
}