using System.Collections.Generic;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToeWeb.ViewModels.User
{
    public class UserIndexViewModel
    {
        public ICollection<UserInfoOutput> AllUsers { get; set; }
    }
}
