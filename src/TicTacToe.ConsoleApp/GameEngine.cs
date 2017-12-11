using System;

namespace TicTacToe.ConsoleApp
{
    public class GameEngine
    {
        public void PrintBoard(string board)
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
    }
}