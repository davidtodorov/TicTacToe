using TicTacToe.Data;
using TicTacToe.Models;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TicTacToeDbContext();

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
                PlayerOne = user1,
                
            };

            context.Games.Add(game1);
            context.SaveChanges();

        }
    }
}
