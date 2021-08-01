namespace Quakkels.Ijdb.Domain.Jokes.SubmitJoke
{
    public class SubmitJokeRequest
    {
        public string Joke { get; }

        public SubmitJokeRequest(string joke)
        {
            Joke = joke;
        }
    }
}