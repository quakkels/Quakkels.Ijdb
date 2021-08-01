using System.Collections.Generic;

namespace Quakkels.Ijdb.Domain.Jokes.SubmitJoke
{
    public class SubmitJokeResult
    {
        public bool Success { get; set; }
        public IJoke Joke { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}