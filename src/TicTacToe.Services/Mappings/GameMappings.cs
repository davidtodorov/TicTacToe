﻿using System;
using System.Linq;
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
                CreationDate = entity.CreationDate,
                CreatorUsername = entity.CreatorUser != null ? entity.CreatorUser.FirstName : null,
                CreatorUserId = entity.CreatorUserId,
                OpponentUsername = entity.OpponentUser != null ? entity.OpponentUser.FirstName : null,
                OpponentUserId = entity.OpponentUserId,
                State = entity.State,
                Visibility = entity.Visibility
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
                State = entity.State,
                Visibility = entity.Visibility
            };

        public static readonly Expression<Func<User, GameScoresInfoOutput>> ToGameScoresOutput =
            entity => new GameScoresInfoOutput()
            {
                Username = entity.FirstName,
                Wins = entity.Scores.Count(s => s.Status == ScoreStatus.Win),
                Loses = entity.Scores.Count(s => s.Status == ScoreStatus.Loss),
                Draws = entity.Scores.Count(s => s.Status == ScoreStatus.Draw),
                Scores = 100 * entity.Scores.Count(s => s.Status == ScoreStatus.Win) 
                         + 30 * entity.Scores.Count(s => s.Status == ScoreStatus.Draw) 
                         + 15 * entity.Scores.Count(s => s.Status == ScoreStatus.Loss)
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
                State = entity.State, 
                Visibility = entity.Visibility
            };
        }
    }
}