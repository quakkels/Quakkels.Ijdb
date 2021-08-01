using System;
using Quakkels.Ijdb.Domain.Jokes;

namespace Quakkels.Ijdb.DataAccess
{
    public class Joke : IJoke
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ApprovedAt { get; set; }
    }
}