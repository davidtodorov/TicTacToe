using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class UserNotification
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
