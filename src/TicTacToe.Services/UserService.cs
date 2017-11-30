using System;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly TicTacToeDbContext context;

        public UserService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public UserRegistrationOutput Register(UserRegistrationInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FirstName))
            {
                throw new ValidationException($"The parameter '{nameof(input.FirstName)}' cannot be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(input.LastName))
            {
                throw new ValidationException($"The parameter '{nameof(input.LastName)}' cannot be null, empty or whitespace.");
            }

            var user = new User()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhotoUrl = input.PhotoUrl
            };

            context.Users.Add(user);
            context.SaveChanges();

            var outputResult = new UserRegistrationOutput()
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhotoUrl = user.PhotoUrl,
                RegistrationDate = user.RegistrationDate
            };

            return outputResult;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}