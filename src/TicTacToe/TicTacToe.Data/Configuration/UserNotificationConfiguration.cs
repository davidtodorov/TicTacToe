using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Models;

namespace TicTacToe.Data.Configuration
{
    class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder
                .HasKey(un => new
                {
                    un.UserId,
                    un.NotificationId
                });

            builder
                .HasOne(un => un.Notification)
                .WithMany(un => un.UsersNotifications)
                .HasForeignKey(un => un.NotificationId);

            builder
                .HasOne(un => un.User)
                .WithMany(un => un.UsersNotifications)
                .HasForeignKey(un => un.UserId);

        }
    }
}
