using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data;
using TicTacToe.Data.Extensions;
using TicTacToe.Models;

namespace Seed._ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TicTacToeDbContextFactory().CreateDbContext();

            if (!context.AllMigrationsApplied())
            {
                context.Database.Migrate();
                Console.WriteLine("Database migrated...");
            }

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            EnsureSeeded(context);
        }

        public static void EnsureSeeded(TicTacToeDbContext context)
        {
            var r = new Random();
            var sw = Stopwatch.StartNew();

            for (int j = 0; j <= 5000; j++)
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

                for (int i = 1; i <= 100; i++)
                {
                    var game = new Game()
                    {
                        Name = $"Game{i}",
                        CreatorUserId = userCreator.Id,
                        OpponentUserId = userOpponent.Id,
                        Visibility = VisibilityType.Public,
                        State = (GameState)r.Next(4, 7)
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
                    Console.WriteLine($"Step: {j}, Elapsed: {sw.Elapsed}");
                    sw = Stopwatch.StartNew();
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
