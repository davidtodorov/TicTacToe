using System;
using TicTacToeWeb.Extensions;

namespace TicTacToeWeb.ViewModels.Game
{
    public class JoinGameViewModel
    {
        [NoEmptyGuid]
        public Guid GameId { get; set; }
    }
}