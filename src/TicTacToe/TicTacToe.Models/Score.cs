using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public ScoreStatus ScoreStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
