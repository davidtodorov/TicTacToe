using System;
using System.Linq.Expressions;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class UserMappings
    {
        public static readonly Expression<Func<User, UserInfoOutput>> ToUserInfoOutput =
            entity => new UserInfoOutput
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl,
                RegistrationDate = entity.RegistrationDate
            };

        public static UserInfoOutput ToUserInfo(this User entity)
        {
            return new UserInfoOutput
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl,
                RegistrationDate = entity.RegistrationDate
            };
        }
    }
}