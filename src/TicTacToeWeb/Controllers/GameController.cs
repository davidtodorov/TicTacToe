using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Interfaces;
using TicTacToeWeb.ViewModels.Game;

namespace TicTacToeWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var viewModel = new GameIndexViewModel()
            {
                AvailableGames = this.gameService.GetAvailableGames(this.User.Identity.GetUserId())
            };

            return View(viewModel);
        }
    }
}