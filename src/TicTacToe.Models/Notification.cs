using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Notification
    {
        public Notification()
        {
            this.CreationDate = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotificationId { get; set; }

        [Range(ValidationConstants.NOTIFICATION_STATE_MIN_VALUE, ValidationConstants.NOTIFICATION_STATE_MAX_VALUE)]
        public NotificationState State { get; set; }
        
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the user's identifier that generates the notification.
        /// </summary>
        [Required]
        public string DestinationUserId { get; set; }

        /// <summary>
        /// Gets or sets the user that receives the notification.
        /// </summary>
        [ForeignKey(nameof(DestinationUserId))]
        public User DestinationUser { get; set; }

        /// <summary>
        /// Gets or sets the user's identifier that receives the notification.
        /// </summary>
        [Required]
        public string SourceUserId { get; set; }

        /// <summary>
        /// Gets or sets the user that receives the notification.
        /// </summary>
        [ForeignKey(nameof(SourceUserId))]
        public User SourceUser { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}