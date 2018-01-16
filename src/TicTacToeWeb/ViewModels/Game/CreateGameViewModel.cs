using System.ComponentModel.DataAnnotations;
using TicTacToe.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class CreateGameViewModel
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        public string Password { get; set; }

        public VisibilityType Visibility { get; set; }

    }
}
