using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces.Models
{
    public class GameCreationInput
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public VisibilityType Visibility { get; set; }

        public GameState State { get; set; }
    }
}