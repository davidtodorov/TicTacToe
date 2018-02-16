using System;
using System.ComponentModel.DataAnnotations;
using TicTacToe.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class PlayGameViewModel
    {
        [Required]
        public Guid GameId { get; set; }
        
        [Range(ValidationConstants.ROW_COL_MIN_lENGTH, ValidationConstants.ROW_COL_MAX_lENGTH)]
        public int Row { get; set; }

        [Range(ValidationConstants.ROW_COL_MIN_lENGTH, ValidationConstants.ROW_COL_MAX_lENGTH)]
        public int Col { get; set; }
    }
}
