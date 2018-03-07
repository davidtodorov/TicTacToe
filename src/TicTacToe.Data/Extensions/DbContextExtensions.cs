using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TicTacToe.Models;

namespace TicTacToe.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this TicTacToeDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var randomNumberGenerator = new Random();
            var stopWatch = Stopwatch.StartNew();

            for (int j = 0; j <= 50; j++)
            {
                var userCreator = new User
                {
                    FirstName = $"Creator{j}",
                    LastName = $"Ivanov"
                };

                var userOpponent = new User
                {
                    FirstName = $"Opponent{j}",
                    LastName = $"Ivanov"
                };

                context.Users.Add(userCreator);
                context.Users.Add(userOpponent);

                for (int i = 1; i <= 50; i++)
                {
                    var game = new Game()
                    {
                        Name = $"Game{i}",
                        CreatorUserId = userCreator.Id,
                        OpponentUserId = userOpponent.Id,
                        Visibility = VisibilityType.Public,
                        State = (GameState)randomNumberGenerator.Next(4, 7)
                    };

                    context.Games.Add(game);

                    if (game.State == GameState.CreatorVictory)
                    {
                        context.Scores.Add(CreateScore(game, game.CreatorUserId, ScoreStatus.Win));
                        context.Scores.Add(CreateScore(game, game.OpponentUserId, ScoreStatus.Loss));
                    }
                    else if (game.State == GameState.OpponentVictory)
                    {
                        context.Scores.Add(CreateScore(game, game.CreatorUserId, ScoreStatus.Loss));
                        context.Scores.Add(CreateScore(game, game.OpponentUserId, ScoreStatus.Win));
                    }
                    else if (game.State == GameState.Draw)
                    {
                        context.Scores.Add(CreateScore(game, game.CreatorUserId, ScoreStatus.Draw));
                        context.Scores.Add(CreateScore(game, game.OpponentUserId, ScoreStatus.Draw));
                    }
                }

                if (j % 100 == 0)
                {
                    context.SaveChanges();
                    Console.WriteLine($"Step: {j}, Elapsed: {stopWatch.Elapsed}");
                    stopWatch = Stopwatch.StartNew();
                }
            }

            context.SaveChanges();
        }

        private static Score CreateScore(Game game, string userId, ScoreStatus status)
        {
            var score = new Score()
            {
                Game = game,
                UserId = userId,
                Status = status
            };

            return score;
        }
    }
}