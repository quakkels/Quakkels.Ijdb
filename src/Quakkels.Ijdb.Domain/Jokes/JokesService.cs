using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Quakkels.Ijdb.Domain.Jokes.SubmitJoke;

namespace Quakkels.Ijdb.Domain.Jokes
{
    public class JokesService : IJokesService
    {
        private readonly IJokesRepo _jokesRepo;

        public JokesService(IJokesRepo jokesRepo)
        {
            _jokesRepo = jokesRepo;
        }

        public async Task<IEnumerable<IJoke>> GetAllJokes()
        {
            var jokes = await _jokesRepo.GetJokes();
            return jokes;
        }

        public async Task<SubmitJokeResult> SubmitJoke(SubmitJokeRequest request)
        {
            if (string.IsNullOrEmpty(request?.Joke))
            {
                return new SubmitJokeResult { Success = false };
            }

            var id = await _jokesRepo.InsertJoke(request.Joke);

            var joke = await _jokesRepo.GetJokeById(id);
            var result = new SubmitJokeResult
            {
                Success = true,
                Joke = joke
            };
            return result;
        }
    }
}