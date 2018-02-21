using System;
using System.Linq;
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

        [HttpGet]
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
        public IActionResult GameList()
        {
            var gameList = new GameIndexViewModel()
            {
                AvailableGames = this.gameService.GetAvailableGames(this.User.Identity.GetUserId()),
                UserGamesInProgress = this.gameService.GetUserGamesInProgress(this.User.Identity.GetUserId()),
                UserJoinedGames = this.gameService.GetUserJoinedGames(this.User.Identity.GetUserId())
            };

            return this.PartialView("_GamesPartial", gameList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateGameViewModel());
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
                Name = input.Name,
                Visibility = input.Visibility,
                Password = input.Password
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
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage);
                }

                var gameJoinInput = new GameJoinInput()
                {
                    GameId = input.GameId,
                    Password =  input.Password
                };

                this.gameService.Join(gameJoinInput, this.User.Identity.GetUserId());

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
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(PlayGameViewModel input)
        {
           try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage);
                }

                this.gameService.Play(input.GameId, User.Identity.GetUserId(), input.Row, input.Col);
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
        public IActionResult Status(Guid id)
        {
            
            try
            {
                var status = gameService.Status(id, User.Identity.GetUserId());

                return this.Json(new { Success = true, status });
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
        public IActionResult Scores()
        {
            var scoreList = this.gameService.GetScores().OrderByDescending(s => s.Scores).Take(10).ToList();
            return View(scoreList);
        }
    }
}