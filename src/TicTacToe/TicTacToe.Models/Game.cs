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
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid  GameId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Board { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        [Required]
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