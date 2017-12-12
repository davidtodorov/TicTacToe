using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data.Extensions;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.Services.Exceptions;
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
                var gameService = new GameService(context, new GameResultValidator());

                Console.WriteLine("Enter your First Name");
                string userFirstName = Console.ReadLine();

                Console.WriteLine("Enter your Last Name");
                string userLastName = Console.ReadLine();

                // Add a new user to the database
                var user = userService.Register(new UserRegistrationInput() { FirstName = userFirstName, LastName = userLastName });
                var currentGameId = GetOrCreateGame(gameService, user.Id);

                PlayGame(currentGameId, user.Id);
            }
        }

        private static void PlayGame(Guid gameId, Guid userId)
        {
            var gameEnginge = new GameEngine();

            while (true)
            {
                using (var context = new TicTacToeDbContextFactory().CreateDbContext())
                {
                    var gameService = new GameService(context, new GameResultValidator());

                    var game = gameService.Status(gameId, userId);
                    gameEnginge.PrintBoard(game.Board);

                    if (game.State == GameState.WaitingForASecondPlayer)
                    {
                        Console.WriteLine("Waiting for a second player...");
                    }
                    else if (game.State == GameState.CreatorVictory) 
                    {
                        Console.WriteLine("Game over!");
                        Console.WriteLine();
                        Console.WriteLine($"{game.CreatorUsername} won!");
                        Console.WriteLine($"{game.OpponentUsername} lost!");
                    }
                    else if (game.State == GameState.OpponentVictory)
                    {
                        Console.WriteLine("Game over!");
                        Console.WriteLine();
                        Console.WriteLine($"{game.OpponentUsername} Won!");
                        Console.WriteLine($"{game.CreatorUsername} Lost!");
                    }
                    else if (game.State == GameState.Draw)
                    {
                        Console.WriteLine("Game over!");
                        Console.WriteLine("It's Draw");
                    }
                    else if ((game.State == GameState.CreatorTurn && game.CreatorUserId == userId) || (game.State == GameState.OpponentTurn && game.OpponentUserId == userId))
                    {
                        Console.WriteLine("It's my turn...");
                        var input = int.Parse(Console.ReadLine());
                        var choosedPosition = ChoosePosition(input);
                        gameService.Play(gameId, userId, choosedPosition.Row, choosedPosition.Col);
                    }
                    else
                    {
                        Console.WriteLine("It's opponent turn...");
                    }
                }

                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private static Guid GetOrCreateGame(GameService gameService, Guid userId)
        {
            Guid currentGameId;
            var availableGames = gameService.GetAvailableGames(userId);

            if (!availableGames.Any())
            {
                Console.WriteLine("There are no available games, please create one");
                Console.Write("Enter name of the game: ");
                var gameName = Console.ReadLine();
                var newGame = new GameCreationInput() { Name = gameName, Visibility = VisibilityType.Public };

                currentGameId = gameService.Create(newGame, userId).Id;
            }
            else
            {
                var games = new List<AvailableGameInfoOutput>();
                int numberOfGame = 1;
                Console.WriteLine("Choose game:");
                foreach (var availableGame in availableGames)
                {
                    Console.WriteLine($"{numberOfGame}. {availableGame.Name}");
                    games.Add(availableGame);
                    numberOfGame++;
                }

                var choosedGame = int.Parse(Console.ReadLine());
                while (choosedGame < 1 || choosedGame > games.Count)
                {
                    Console.WriteLine("Enter valid number");
                    choosedGame = int.Parse(Console.ReadLine());
                }

                currentGameId = gameService.Join(games[choosedGame-1].Id, userId).Id;
            }

            return currentGameId;
        }

        private static (int Row, int Col) ChoosePosition(int number)
        {
            switch (number)
            {
                case 1: return (2, 0);
                case 2: return (2, 1);
                case 3: return (2, 2);

                case 4: return (1, 0);
                case 5: return (1, 1);
                case 6: return (1, 2);

                case 7: return (0, 0);
                case 8: return (0, 1);
                case 9: return (0, 2);
            }

            throw new ValidationException("Invalid input.");
        }
    }
}