using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Models;

namespace TicTacToe.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .Property(game => game.GameId)
                .ValueGeneratedOnAdd();

            builder
                .Property(game => game.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(g => g.Score)
                .WithOne(g => g.Game)
                .HasForeignKey<Score>(g => g.ScoreId);

            
        }
    }
}
