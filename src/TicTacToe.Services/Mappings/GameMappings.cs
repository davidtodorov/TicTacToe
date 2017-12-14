using System;
using System.Linq.Expressions;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class GameMappings
    {
        public static readonly Expression<Func<Game, AvailableGameInfoOutput>> ToAvailableGameInfoOutput =
            entity => new AvailableGameInfoOutput
            {
                Id = entity.GameId,
                Name = entity.Name,
                CreatorUsername = entity.CreatorUser != null ? entity.CreatorUser.FirstName : null,
                OpponentUsername = entity.OpponentUser != null ? entity.OpponentUser.FirstName : null,
                State = entity.State
            };

        public static readonly Expression<Func<Game, GameStatusOutput>> ToGameStatusOutput =
            entity => new GameStatusOutput()
            {
                Id = entity.GameId,
                CreatorUserId = entity.CreatorUserId,
                OpponentUserId = entity.OpponentUserId,
                CreatorUsername = entity.CreatorUser != null ? entity.CreatorUser.FirstName : null,
                OpponentUsername = entity.OpponentUser != null ? entity.OpponentUser.FirstName : null,
                Board = entity.Board,
                State = entity.State
            };

        public static GameStatusOutput ToGameStatus(this Game entity)
        {
            return new GameStatusOutput()
            {
                Id = entity.GameId,
                CreatorUserId = entity.CreatorUserId,
                OpponentUserId = entity.OpponentUserId,
                CreatorUsername = entity.CreatorUser?.FirstName,
                OpponentUsername = entity.OpponentUser?.FirstName,
                Board = entity.Board,
                State = entity.State
            };
        }
    }
}