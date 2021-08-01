using System;

namespace Quakkels.Ijdb.Domain.Jokes
{
    public interface IJoke
    {
        public int Id { get; }
        public string Line1 { get; }
        public DateTime CreatedAt { get; }
        public DateTime ApprovedAt { get; }
    }
}