using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Models;

namespace TicTacToe.Data.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder
                .Property(notf => notf.NotificationId)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(n => n.Game)
                .WithMany(n => n.Notifications)
                .HasForeignKey(n => n.GameId);

            builder
                .HasOne(n => n.DestinationUser)
                .WithMany(n => n.Notifications)
                .HasForeignKey(n => n.NotificationId);

            
        }
    }
}
