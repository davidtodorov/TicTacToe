using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class Notification
    {
        public Notification()
        {
            this.UsersNotifications =  new List<UserNotification>();
        }

        public int Id { get; set; }
        public NotificationState NotificationState { get; set; }
        public string DateAndTime { get; set; }


        public int DestinationUserId { get; set; }
        public int SourceUserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<UserNotification> UsersNotifications { get; set; }
    }
}
