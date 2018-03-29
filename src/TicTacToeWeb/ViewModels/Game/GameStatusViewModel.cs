using System;
using TicTacToe.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class GameStatusViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Board { get; set; }

        public GameState State { get; set; }

        public VisibilityType Visibility { get; set; }

        public string CreatorUsername { get; set; }

        public string CreatorUserId { get; set; }

        public string OpponentUsername { get; set; }

        public string UserId { get; set; }

        public string PrivateJoinLink { get; set; }
    }
}
