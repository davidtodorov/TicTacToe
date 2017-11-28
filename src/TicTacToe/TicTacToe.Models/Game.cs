using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid  GameId { get; set; }

        // TODO: Add validation
        public string Name { get; set; }

        // TODO: Add validation
        public string Board { get; set; }

        // TODO: Add validation
        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        // TODO: Add validation
        public VisibilityType Visibility { get; set; }

        // TODO: Add validation
        public GameState State { get; set; }

        public Guid PlayerOneId { get; set; }

        [ForeignKey(nameof(PlayerOneId))]
        public User PlayerOne { get; set; }

        public Guid? PlayerTwoId { get; set; }

        [ForeignKey(nameof(PlayerTwoId))]
        public User PlayerTwo { get; set; }

        public Guid ScoreId { get; set; }

        [ForeignKey(nameof(ScoreId))]
        public Score Score { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}