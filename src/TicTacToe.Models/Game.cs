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
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]        
        public string Board { get; set; }
        
        [MaxLength(50)]
        public string HashedPassword { get; set; }
        
        public DateTime CreationDate { get; set; }

        [Required]
        [Range(1, 3)]
        public VisibilityType Visibility { get; set; }

        [Range(1, 6)]
        public GameState State { get; set; }

        [Required]
        public string CreatorUserId { get; set; }

        [ForeignKey(nameof(CreatorUserId))]
        public User CreatorUser { get; set; }

        public string OpponentUserId { get; set; }

        [ForeignKey(nameof(OpponentUserId))]
        public User OpponentUser { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}