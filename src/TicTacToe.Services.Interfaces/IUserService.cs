using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Register a user by the given parameters.
        /// </summary>
        /// <param name="input">The user's input data.</param>
        /// <returns>The created user's data.</returns>
        UserRegistrationOutput Register(UserRegistrationInput input);
    }
}