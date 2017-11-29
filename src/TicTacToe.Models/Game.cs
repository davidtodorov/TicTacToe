using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        private string hashedPassword;

        public Game()
        {
            this.Board = "---------";
            this.CreationDate = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Board { get; set; }

        [MaxLength(50)]
        public string HashedPassword
        {
            get
            {
                return this.hashedPassword;
            }
            set
            {
                if (Visibility == VisibilityType.Protected)
                {
                    this.hashedPassword = value;
                }
                else
                {
                    this.hashedPassword = null;
                }
            }
        }
        
        public DateTime CreationDate { get; set; }

        [Required]
        [Range(1, 3)]
        public VisibilityType Visibility { get; set; }

        [Range(1, 6)]
        public GameState State { get; set; }

        public Guid CreatorUserId { get; set; }

        [ForeignKey(nameof(CreatorUserId))]
        public User CreatorUser { get; set; }

        public Guid? OpponentUserId { get; set; }

        [ForeignKey(nameof(OpponentUserId))]
        public User OpponentUser { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}