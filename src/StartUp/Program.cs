using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Start by reading the configuration
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            var context = new TicTacToeDbContext(optionsBuilder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var user1 = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
            };

            var user2 = new User
            {
                FirstName = "Pesho",
                LastName = "Ivanov",
                PhotoUrl = "LinkOfPhoto"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

            var game1 = new Game
            {
                Name = "MostEpicMoment",
                State = GameState.WaitingForASecondPlayer,
                Visibility = VisibilityType.Public,
                CreatorUser = user1,
            };

            context.Games.Add(game1);
            context.SaveChanges();
        }
    }
}
