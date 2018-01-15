using System.Collections.Generic;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class GameIndexViewModel
    {
        public ICollection<AvailableGameInfoOutput> AvailableGames { get; set; }
    }
}