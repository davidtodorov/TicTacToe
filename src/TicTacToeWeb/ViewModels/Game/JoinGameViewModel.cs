using System;
using System.ComponentModel.DataAnnotations;
using TicTacToe.Models;
using TicTacToeWeb.Attributes;

namespace TicTacToeWeb.ViewModels.Game
{
    public class JoinGameViewModel
    {
        [NoEmptyGuid]
        public Guid GameId { get; set; }

        [Range(ValidationConstants.PASSWORD_MIN_LENGTH, ValidationConstants.PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }
    }
}