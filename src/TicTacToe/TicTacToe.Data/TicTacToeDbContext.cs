using System;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;
using TicTacToe.Data.Configuration;

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
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserNotificationConfiguration());

        }
    }
}
