using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicTacToe.Data.Configuration;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class TicTacToeDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}