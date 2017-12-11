using System;
using System.Linq.Expressions;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class UserMappings
    {
        public static readonly Expression<Func<User, UserInfoOutput>> ToUserInfoOutput = entity => entity.ToUserInfo();

        public static UserInfoOutput ToUserInfo(this User entity)
        {
            return new UserInfoOutput
            {
                Id = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl
            };
        }
    }
}