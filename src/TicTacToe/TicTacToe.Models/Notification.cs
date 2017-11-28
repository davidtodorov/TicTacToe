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

        /// <summary>
        /// The user's identifier that generates the notification.
        /// </summary>
        public Guid DestinationUserId { get; set; }

        /// <summary>
        /// The user that receives the notification.
        /// </summary>
        [ForeignKey(nameof(DestinationUserId))]
        public User DestinationUser { get; set; }

        /// <summary>
        /// The user's identifier that receives the notification.
        /// </summary>
        public Guid SourceUserId { get; set; }

        /// <summary>
        /// The user that receives the notification.
        /// </summary>
        [ForeignKey(nameof(SourceUserId))]
        public User SourceUser { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}