using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotificationId { get; set; }

        public NotificationState State { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        public Guid DestinationUserId { get; set; }

        [ForeignKey(nameof(DestinationUserId))]
        public User DestinationUser { get; set; }

        //public Guid SourceUserId { get; set; }

        //[ForeignKey(nameof(SourceUserId))]
        //public User SourceUser { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}