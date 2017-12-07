using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data.Extensions;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new TicTacToeDbContextFactory().CreateDbContext())
            {
                if (!context.AllMigrationsApplied())
                {
                    context.Database.Migrate();
                    context.EnsureSeeded();
                    Console.WriteLine("Database migrated...");
                }

                var userService = new UserService(context);
                var gameService = new GameService(context);

                // Add a new user to the database
                var user1 = new UserRegistrationInput() { FirstName = "Test", LastName = "User" };
                var user1Result = userService.Register(user1);

                // Prints all games from the datebase
                var games = gameService.GetAvailableGames(user1Result.Id);
                if (games.Count == 0)
                {
                    // Creating new game
                    var newGame = new GameCreationInput()
                    {
                        Name = "Game1",
                        Visibility = VisibilityType.Public
                    };
                    var createdGame = gameService.Create(newGame, user1Result.Id);
                    games = gameService.GetAvailableGames(user1Result.Id);
                }

                Console.WriteLine($"All games: {string.Join(", ", games.Select(g => g.Name))}");

                var joinGame = gameService.Join(games.FirstOrDefault().Id, user1Result.Id);
            }
        }
    }
}