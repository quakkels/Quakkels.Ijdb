using System.Collections.Generic;
using System.Linq;
using Moq;
using Quakkels.Ijdb.DataAccess;
using Quakkels.Ijdb.Domain.Jokes;
using Quakkels.Ijdb.Domain.Jokes.SubmitJoke;
using Xunit;

namespace Quakkels.Ijdb.UnitTests.DataAccess
{
    public class JokesServiceTests
    {
        private readonly Mock<IJokesRepo> _jokesRepo;
        private readonly IJokesService _target;

        public JokesServiceTests()
        {
            _jokesRepo = new Mock<IJokesRepo>();
            _jokesRepo
                .Setup(x =>
                    x.InsertJoke("joke"))
                .ReturnsAsync(11);
            _jokesRepo
                .Setup(x =>
                    x.GetJokeById(11))
                .ReturnsAsync(new Joke {Id = 11, Line1 = "joke"});
            _jokesRepo
                .Setup(x =>
                    x.GetJokes())
                .ReturnsAsync(new List<IJoke>
                {
                    new Joke {Id = 11, Line1 = "joke"},
                    new Joke {Id = 12, Line1 = "joke2"}
                });
            
            _target = new JokesService(_jokesRepo.Object);
        }
        
        [Fact]
        public async void CanSubmitJoke()
        {
            // arrange
            var request = new SubmitJokeRequest("joke");
            
            // act
            var result = await _target.SubmitJoke(request);
            
            // assert
            Assert.True(result.Success);
            Assert.Equal("joke", result.Joke.Line1);
        }

        [Fact]
        public async void CanGetAllJokes()
        {
            // act
            var result = await _target.GetAllJokes();
            
            // assert
            Assert.Equal(2, result.Count());
        }
    }
}