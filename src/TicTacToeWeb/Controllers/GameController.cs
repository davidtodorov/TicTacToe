using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Exceptions;
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
                return View(input);
            }

            var gameCreationInput = new GameCreationInput()
            {
                Name = input.Name
            };

            var createdGame = this.gameService.Create(gameCreationInput, this.User.Identity.GetUserId());

            return RedirectToAction(nameof(Play), new
            {
                Id = createdGame.Id
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(JoinGameViewModel input)
        {
            try
            {
                this.gameService.Join(input.GameId, this.User.Identity.GetUserId());
                return this.Json(new {Success = true});
            }
            catch (Exception e)
            {
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";
                
                return this.Json(new
                {
                    Success = false,
                    Exception = exceptionMessage
                });
            }
        }

        [HttpGet]
        public IActionResult Play(Guid id)
        {
            var game = this.gameService.Status(id, User.Identity.GetUserId());

            var statusGame = new GameStatusViewModel()
            {
                Id = game.Id,
                CreatorUsername = game.CreatorUsername,
                OpponentUsername = game.OpponentUsername,
                Board = game.Board,
                State = game.State
            };

            return View(statusGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(PlayGameViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            this.gameService.Play(input.GameId, input.UserId, input.Row, input.Col);

            return View();
        }


    }
}