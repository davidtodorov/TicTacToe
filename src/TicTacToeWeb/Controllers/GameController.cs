using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;
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
                AvailableGames = this.gameService.GetAvailableGames(this.User.Identity.GetUserId()),
                UserGamesInProgress = this.gameService.GetUserGamesInProgress(this.User.Identity.GetUserId()),
                UserJoinedGames = this.gameService.GetUserJoinedGames(this.User.Identity.GetUserId())
            };

            return View(viewModel);
        }

        public IActionResult Play()
        {
            return null;
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameViewModel input)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Should show the error on the same page
                return Redirect("/Game/Create");
            }

            var gameCreationInput = new GameCreationInput()
            {
                Name = input.Name
            };

            this.gameService.Create(gameCreationInput, this.User.Identity.GetUserId());

            return RedirectToAction(nameof(Index));
            //return RedirectToAction(nameof(Play));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(JoinGameViewModel input)
        {
            this.gameService.Join(input.GameId, input.UserId);

            return RedirectToAction(nameof(Play));
        }


    }
}