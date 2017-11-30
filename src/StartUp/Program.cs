using TicTacToe.ConsoleApp.Configuration;
using TicTacToe.Models;

namespace TicTacToe.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var context = new TicTacToeDbContextFactory().CreateDbContext();

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
