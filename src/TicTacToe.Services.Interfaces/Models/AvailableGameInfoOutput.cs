using System;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces.Models
{
    public class AvailableGameInfoOutput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CreatorUsername { get; set; }

        public string OpponentUsername { get; set; }

        public GameState State { get; set; }
    }
}