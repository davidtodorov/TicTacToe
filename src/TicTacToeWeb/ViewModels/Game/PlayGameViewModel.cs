using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToeWeb.ViewModels.Game
{
    public class PlayGameViewModel
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
