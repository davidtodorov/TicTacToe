using System;

namespace TicTacToe.Services.Interfaces.Models
{
    public class GameJoinInput
    {
        public Guid GameId { get; set; }

        public string Password { get; set; }
    }
}
