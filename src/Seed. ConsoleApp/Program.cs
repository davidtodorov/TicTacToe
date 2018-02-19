using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data;
using TicTacToe.Data.Extensions;
using TicTacToe.Models;
using TicTacToe.Services;

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

            EnsureSeeded(context);

            var gameResultValidator = new GameResultValidator();
            var gameService = new GameService(context, gameResultValidator);
            var games = context.Games.ToList();

            foreach (var game in games)
            {
                if (game.State == GameState.CreatorVictory)
                {
                    gameService.CreateScore(game, game.CreatorUserId, ScoreStatus.Win);
                    gameService.CreateScore(game, game.OpponentUserId, ScoreStatus.Loss);
                }
                else if (game.State == GameState.OpponentVictory)
                {
                    gameService.CreateScore(game, game.CreatorUserId, ScoreStatus.Loss);
                    gameService.CreateScore(game, game.OpponentUserId, ScoreStatus.Win);
                }
                else if (game.State == GameState.Draw)
                {
                    gameService.CreateScore(game, game.CreatorUserId, ScoreStatus.Draw);
                    gameService.CreateScore(game, game.OpponentUserId, ScoreStatus.Draw);
                }

                context.SaveChanges();
            }
        }

        public static void EnsureSeeded(TicTacToeDbContext context)
        {
            for (int i = 1; i <= 100; i++)
            {
                var rand = new Random();
                var user1 = new User
                {
                    FirstName = $"User{i}",
                    LastName = "Ivanov",
                };

                var user2 = new User
                {
                    FirstName = $"User{i+1}",
                    LastName = "Ivanov",
                };

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.SaveChanges();

                var userList = context.Users.ToList();
                userList.Remove(user1);

                var game = new Game
                {
                    Name = $"Game{i}",
                    State = (GameState)rand.Next(4, 7),
                    Visibility = VisibilityType.Public,
                    CreatorUserId = user1.Id,
                    OpponentUserId = userList[rand.Next(0, userList.Count)].Id
                };

                context.Games.Add(game);
                context.SaveChanges();
            }
        }
    }
}
