using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public NotificationState NotificationState { get; set; }
        public string DateAndTime { get; set; }


        public Guid DestinationUserId { get; set; }
        public Guid SourceUserId { get; set; }
        public User User { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
