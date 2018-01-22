using System.ComponentModel.DataAnnotations;
using TicTacToe.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class CreateGameViewModel
    {
        [Required]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        
        public VisibilityType Visibility { get; set; }
    }
}
