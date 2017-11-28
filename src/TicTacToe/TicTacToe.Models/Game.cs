using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        public Game()
        {
            this.Board = "---------";
            this.CreationDate = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid  GameId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Board { get; set; }

        [Required]
        [NotMapped]
        [MaxLength(50)]
        public string Password
        {
            get { return this.Password; }
            set
            {
                if ((int)Visibility == 1 || (int)Visibility == 2 )
                {
                    this.Password = value;
                }
                else
                {
                    this.Password = null;
                }
            } 

        }

        public DateTime CreationDate { get; set; }

        [Required]
        [Range(0, 2)]
        public VisibilityType Visibility { get; set; }

        [Range(1, 6)]
        public GameState State { get; set; }

        public Guid PlayerOneId { get; set; }

        [ForeignKey(nameof(PlayerOneId))]
        public User PlayerOne { get; set; }

        public Guid? PlayerTwoId { get; set; }

        [ForeignKey(nameof(PlayerTwoId))]
        public User PlayerTwo { get; set; }


        public ICollection<Notification> Notifications { get; set; }
    }
}