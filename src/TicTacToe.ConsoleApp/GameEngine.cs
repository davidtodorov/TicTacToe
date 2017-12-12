using System;
using System.Linq;
using System.Threading;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.ConsoleApp
{
    public class GameEngine
    {
        public void PlayGame(Guid gameId, Guid userId)
        {
            while (true)
            {
                using (var context = new TicTacToeDbContextFactory().CreateDbContext())
                {
                    var gameService = new GameService(context, new GameResultValidator());

                    var game = gameService.Status(gameId, userId);
                    this.PrintBoard(game.Board);

                    if (game.State == GameState.WaitingForASecondPlayer)
                    {
                        Console.WriteLine("Waiting for a second player...");
                    }
                    else if (game.State == GameState.CreatorVictory)
                    {
                        Console.WriteLine("Game over! \n");
                        Console.WriteLine($"{game.CreatorUsername} won!");
                        Console.WriteLine($"{game.OpponentUsername} lost!");
                    }
                    else if (game.State == GameState.OpponentVictory)
                    {
                        Console.WriteLine("Game over! \n");
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

                        while (int.TryParse(Console.ReadLine(), out var input))
                        {
                            var choosedPosition = ChoosePosition(input);
                            gameService.Play(gameId, userId, choosedPosition.Row, choosedPosition.Col);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("It's opponent turn...");
                    }
                }

                Thread.Sleep(250);
                Console.Clear();
            }
        }

        public Guid GetOrCreateGame(Guid userId)
        {
            using (var context = new TicTacToeDbContextFactory().CreateDbContext())
            {
                var gameService = new GameService(context, new GameResultValidator());
                var availableGames = gameService.GetAvailableGames(userId).ToList();

                if (!availableGames.Any())
                {
                    Console.WriteLine("There are no available games, please create one");
                    Console.Write("Enter name of the game: ");

                    var gameName = Console.ReadLine();
                    var newGame = new GameCreationInput() { Name = gameName, Visibility = VisibilityType.Public };

                    return gameService.Create(newGame, userId).Id;
                }
                else
                {
                    Console.WriteLine("Choose game:");

                    for (int i = 0; i < availableGames.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {availableGames[i].Name}");
                    }

                    int chosenGame;
                    while (!int.TryParse(Console.ReadLine(), out chosenGame) || chosenGame < 1 || chosenGame > availableGames.Count)
                    {
                        Console.WriteLine("Enter valid number");
                    }

                    return gameService.Join(availableGames[chosenGame - 1].Id, userId).Id;
                }
            }
        }

        private void PrintBoard(string board)
        {
            Console.WriteLine(new string('-', 9));

            for (int i = 0, cell = 0; i < 3; i++)
            {
                Console.Write("| ");

                for (var j = 0; j < 3; j++)
                {
                    Console.Write($"{board[cell++]} ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', 9));
        }

        private (int Row, int Col) ChoosePosition(int number)
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