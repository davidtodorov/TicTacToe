﻿using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
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

            return RedirectToAction(nameof(Play), new { Id = createdGame.Id });
        }

        [HttpGet]
        public IActionResult Join(Guid id)
        {
            try
            {
                this.gameService.Join(new GameJoinInput() { GameId = id }, User.Identity.GetUserId());

                return RedirectToAction(nameof(Play), new { id = id });
            }
            catch (Exception e)
            {
                return RedirectToAction("CustomError", "Error");
            }
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

                string url = Url.Action("Join", new RouteValueDictionary(new JoinGameViewModel() { GameId = input.GameId }));

                var gameJoinInput = new GameJoinInput()
                {
                    GameId = input.GameId,
                    Password = input.Password
                };
        
                this.gameService.Join(gameJoinInput, this.User.Identity.GetUserId());

                return this.Json(new { Success = true });
            }
            catch (Exception e)
            {
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";
                
                return this.Json(new { Success = false, Exception = exceptionMessage });
            }
        }

        [HttpGet]
        public IActionResult Play(Guid id)
        {
            try
            {
                var url = new UrlHelper(this.ControllerContext);
                var game = this.gameService.Status(id, User.Identity.GetUserId());

                var statusGame = new GameStatusViewModel()
                {
                    Id = game.Id,
                    Name = game.Name,
                    CreatorUsername = game.CreatorUsername,
                    CreatorUserId = game.CreatorUserId,
                    OpponentUsername = game.OpponentUsername,
                    Board = game.Board,
                    State = game.State,
                    Visibility = game.Visibility,
                    UserId = User.Identity.GetUserId(),
                    PrivateJoinLink = Url.Action("Join", "Game", new { id = game.Id }, Request.Scheme)
                };

                return View(statusGame);
            }
            catch (Exception)
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
                return this.Json(new { Success = true });
            }
            catch (Exception e)
            {
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";

                return this.Json(new { Success = false, Exception = exceptionMessage });
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

                return this.Json(new { Success = false, Exception = exceptionMessage });
            }
        }
    }
}