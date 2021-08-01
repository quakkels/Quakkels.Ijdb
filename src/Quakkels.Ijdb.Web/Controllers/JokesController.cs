using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quakkels.Ijdb.Domain.Jokes;
using Quakkels.Ijdb.Domain.Jokes.SubmitJoke;
using Quakkels.Ijdb.Web.Models;

namespace Quakkels.Ijdb.Web.Controllers
{
    public class JokesController : Controller
    {
        private readonly IJokesService _jokesService;
        private readonly ILogger<JokesController> _logger;

        public JokesController(
            IJokesService jokesService,
            ILogger<JokesController> logger)
        {
            _jokesService = jokesService;

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var jokes = await _jokesService.GetAllJokes();
            var viewModel = jokes.Select(joke => new JokeViewModel {Joke = joke.Line1}).ToList();

            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string joke)
        {
            if (string.IsNullOrEmpty(joke))
            {
                ViewBag.Message = "You call that a joke?";
                return View();
            }

            var result = await _jokesService.SubmitJoke(new SubmitJokeRequest(joke));

            if (!result.Success)
            {
                ViewBag.Message = string.Join(' ', result.ErrorMessages.Select(x => x));
                return View();
            }

            ViewBag.Message = "Thanks for submitting a... I guess this could be a joke. Keep at it, tiger.";
            return RedirectToAction(nameof(Index));
        }
}
}