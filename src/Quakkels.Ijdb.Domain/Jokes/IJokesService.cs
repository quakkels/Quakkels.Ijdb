using System.Collections.Generic;
using System.Threading.Tasks;
using Quakkels.Ijdb.Domain.Jokes.SubmitJoke;

namespace Quakkels.Ijdb.Domain.Jokes
{
    public interface IJokesService
    {
        Task<IEnumerable<IJoke>> GetAllJokes();
        Task<SubmitJokeResult> SubmitJoke(SubmitJokeRequest request);
    }
}