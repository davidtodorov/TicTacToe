using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Models;

namespace TicTacToe.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(user => user.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(user => user.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(user => user.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
