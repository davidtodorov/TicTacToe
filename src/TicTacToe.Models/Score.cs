﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Score
    {
        public Score()
        {
            this.ScoreId = Guid.NewGuid();
        }

        [Key]
        public Guid ScoreId { get; set; }

        [Range(1, 3)]
        public ScoreStatus ScoreStatus { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}