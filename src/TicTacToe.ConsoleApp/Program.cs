using System;
using System.Linq;
using System.Threading;
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
                var user = userService.Register(new UserRegistrationInput() { FirstName = "Test3", LastName = "User" });
                var currentGameId = GetOrCreateGame(gameService, user.Id);

                PlayGame(gameService, currentGameId, user.Id);
            }
        }

        private static void PlayGame(GameService gameService, Guid gameId, Guid userId)
        {
            while (true)
            {
                var game = gameService.Status(gameId, userId);

                if (game.State == GameState.WaitingForASecondPlayer)
                {
                    Console.WriteLine("Waiting for a second player...");
                }
                else if (game.State == GameState.CreatorVictory || game.State == GameState.OpponentVictory || game.State == GameState.Draw)
                {
                    Console.WriteLine("Game over!");
                }
                else if (game.State == GameState.CreatorTurn && game.CreatorUserId == userId)
                {
                    Console.WriteLine("It's my turn...");
                }
                else if (game.State == GameState.OpponentTurn && game.OpponentUserId == userId)
                {
                    Console.WriteLine("It's my turn...");
                }
                else
                {
                    Console.WriteLine("It's opponent turn...");
                }

                Thread.Sleep(1000);
            }
        }

        private static Guid GetOrCreateGame(GameService gameService, Guid userId)
        {
            Guid currentGameId;
            var games = gameService.GetAvailableGames(userId);

            if (!games.Any())
            {
                var newGame = new GameCreationInput() { Name = "Game2", Visibility = VisibilityType.Public };

                currentGameId = gameService.Create(newGame, userId).Id;
            }
            else
            {
                currentGameId = gameService.Join(games.FirstOrDefault().Id, userId).Id;
            }

            return currentGameId;
        }
    }
}