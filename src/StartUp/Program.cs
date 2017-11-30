using System;
using Microsoft.EntityFrameworkCore;
using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Data;

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
