using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicTacToeWeb.ViewModels;

namespace TicTacToeWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult CustomError()
        {
            return View();
        }
    }
}