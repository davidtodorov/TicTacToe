using System;
using TicTacToe.Data;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TicTacToeDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }
    }
}
