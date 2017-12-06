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
                var newUser = new UserRegistrationInput() { FirstName = "Test", LastName = "User" };
                var result = userService.Register(newUser);

                // Prints all users from the database
                var users = userService.All();
                Console.WriteLine($"All users: {string.Join(", ", users.Select(x => x.FirstName + " " + x.LastName))}");

                // Creating new game
                var newGame = new GameCreationInput()
                {
                    Name = "Game1",
                    Visibility = VisibilityType.Public
                };
                var gameResult = gameService.Create(newGame, result.Id);

               // Prints all games from the datebase
               var games = gameService.GetAvailableGames(result.Id);
                Console.WriteLine($"All games: {string.Join(", ", games.Select(g=> g.Name))}");
            }
        }
    }
}