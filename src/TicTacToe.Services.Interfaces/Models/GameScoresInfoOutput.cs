﻿namespace TicTacToe.Services.Interfaces.Models
{
    public class GameScoresInfoOutput
    {
        public string Username { get; set; }

        public int Wins { get; set; }

        public int Loses { get; set; }

        public int Draws { get; set; }

        public int Points { get; set; }
    }
}
