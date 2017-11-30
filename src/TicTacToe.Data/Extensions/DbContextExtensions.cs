using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TicTacToe.Models;

namespace TicTacToe.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                                 .GetAppliedMigrations()
                                 .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                               .Migrations
                               .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this TicTacToeDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            var user1 = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov"
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
                CreatorUser = user1
            };

            context.Games.Add(game1);
            context.SaveChanges();
        }
    }
}