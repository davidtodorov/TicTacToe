using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class Score
    {
        public Guid Id { get; set; }
        public ScoreStatus Status { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
