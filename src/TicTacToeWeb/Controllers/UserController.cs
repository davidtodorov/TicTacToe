using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Interfaces;
using TicTacToeWeb.ViewModels.User;

namespace TicTacToeWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var viewModel = new UserIndexViewModel()
            {
                AllUsers = this.userService.AllUsers()
            };

            return View(viewModel);
        }
    }
}