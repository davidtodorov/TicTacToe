using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class Notification
    {
        public Notification()
        {
        }

        public int NotificationId { get; set; }
        public NotificationState NotificationState { get; set; }
        public string DateAndTime { get; set; }

        public int DestinationUserId { get; set; }
        public int SourceUserId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
        
    }
}
