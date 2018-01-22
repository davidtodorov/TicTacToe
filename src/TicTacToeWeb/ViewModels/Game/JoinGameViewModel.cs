using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToeWeb.ViewModels.Game
{
    public class JoinGameViewModel
    {
        public Guid GameId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
