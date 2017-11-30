using System;
using System.Linq.Expressions;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Mappings
{
    internal class UserMappings
    {
        public static readonly Expression<Func<User, UserRegistrationOutput>> ToUserRegistrationOutput =
            entity => new UserRegistrationOutput
            {
                Id = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl,
                RegistrationDate = entity.RegistrationDate
            };
    }
}