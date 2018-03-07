using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoreService scoreService;

        public ScoresController(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var scoreList = this.scoreService.GetScores();
            return View();
        }
    }
}
