using System;
using TicTacToeWeb.Extensions;

namespace TicTacToeWeb.ViewModels.Game
{
    public class JoinGameViewModel
    {
        [GuidValdationAttribute.NoEmptyGuid]
        public Guid GameId { get; set; }
    }
}