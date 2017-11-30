using System;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data;
using TicTacToe.Data.Extensions;

namespace TicTacToe.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var context = new TicTacToeDbContextFactory().CreateDbContext();

            if (!context.AllMigrationsApplied())
            {         
                context.Database.Migrate();
                context.EnsureSeeded();

                Console.WriteLine("Database migrated...");
            }

            Console.WriteLine("Finished...");
        }
    }
}
