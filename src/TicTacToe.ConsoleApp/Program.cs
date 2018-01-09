using System;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data;
using TicTacToe.Data.Extensions;
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
                    Console.WriteLine("Database migrated...");
                }
                
                // Add a new user to the database
                var user = RegisterUser(context);
                StartNewGame(user.Id);
            }
        }

        private static UserInfoOutput RegisterUser(TicTacToeDbContext context)
        {
            Console.WriteLine("Enter your First Name");
            string userFirstName = Console.ReadLine();

            Console.WriteLine("Enter your Last Name");
            string userLastName = Console.ReadLine();

            var userService = new UserService(context);
            return userService.Register(new UserRegistrationInput() { FirstName = userFirstName, LastName = userLastName });
        }

        private static void StartNewGame(Guid userId)
        {
            var gameEngine = new GameEngine();

            var currentGameId = gameEngine.GetOrCreateGame(userId);
            gameEngine.PlayGame(currentGameId, userId);
        }
    }
}