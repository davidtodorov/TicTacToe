﻿using System;
using System.Linq.Expressions;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class GameMappings
    {
        public static readonly Expression<Func<Game, AvailableGameInfoOutput>> ToAvailableGameInfoOutput = entity => entity.ToAvailableGameInfo();

        public static AvailableGameInfoOutput ToAvailableGameInfo(this Game entity)
        {
            return new AvailableGameInfoOutput
            {
                Id = entity.GameId,
                Name = entity.Name,
                CreatorUsername = entity.CreatorUser?.FirstName + " " + entity.CreatorUser?.LastName,
                OpponentUsername = entity.OpponentUser?.FirstName + " " + entity.OpponentUser?.LastName,
                State = entity.State
            };
        }
    }
}