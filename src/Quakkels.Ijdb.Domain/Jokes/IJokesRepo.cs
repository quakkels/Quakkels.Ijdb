using System.Collections.Generic;
using System.Threading.Tasks;
using Quakkels.Ijdb.Domain.Jokes.SubmitJoke;

namespace Quakkels.Ijdb.Domain.Jokes
{
    public interface IJokesRepo
    {
        Task<IEnumerable<IJoke>> GetJokes();
        Task<IJoke> GetJokeById(int id);
        Task<int> InsertJoke(string joke);
    }
}