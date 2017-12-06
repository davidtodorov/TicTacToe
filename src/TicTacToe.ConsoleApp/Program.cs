using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
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
                    context.EnsureSeeded();

                    Console.WriteLine("Database migrated...");
                }

                var userService = new UserService(context);
                var gameService = new GameService(context);

                // Add a new user to the database
                var newUser = new UserRegistrationInput() { FirstName = "Test", LastName = "User" };
                userService.Register(newUser);

                // Prints all users from the database
                var users = userService.All();
                Console.WriteLine($"All users: {string.Join(", ", users.Select(x => x.FirstName + " " + x.LastName))}");
            }
        }
    }
}