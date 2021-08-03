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

        private const string SuccessMessage =
            "Thanks for submitting a... I guess this could be a joke. Keep at it, tiger.";

        private const string FailureMessage = "You call that a joke?";

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
        public async Task<IActionResult> Submit(SubmitJokeViewModel model)
        {
            if (string.IsNullOrEmpty(model.Joke))
            {
                ViewBag.Message = FailureMessage;
                return View();
            }

            // trap spammers
            if (!string.IsNullOrEmpty(model.Name) || !string.IsNullOrEmpty(model.Email))
            {
                _logger.LogWarning("Honeypot captured spam");
                ViewBag.Message = SuccessMessage; // show fake success
                return RedirectToAction(nameof(Index));
            }

            var result = await _jokesService.SubmitJoke(new SubmitJokeRequest(model.Joke));

            if (!result.Success)
            {
                ViewBag.Message = string.Join(' ', result.ErrorMessages.Select(x => x));
                return View();
            }

            ViewBag.Message = SuccessMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}