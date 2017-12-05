﻿using System;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces.Models
{
    public class GameStatusOutput
    {
        public Guid Id { get; set; }

        public string Board { get; set; }

        public GameState State { get; set; }

        public string CreatorUsername { get; set; }

        public string OpponentUsername { get; set; }
    }
}