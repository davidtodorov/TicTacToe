using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;
using TicTacToe.Services.Mappings;

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
        public ICollection<UserInfoOutput> All()
        {
            var users = this.context.Users.Select(UserMappings.ToUserInfoOutput).ToList();

            return users;
        }

        /// <inheritdoc />
        public UserInfoOutput Register(UserRegistrationInput input)
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

            return user.ToUserInfo();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}