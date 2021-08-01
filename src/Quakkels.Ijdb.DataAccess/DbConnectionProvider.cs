using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Quakkels.Ijdb.DataAccess
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;
        public DbConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("InternetJokeDatabase");
        }

        public IDatabaseWrapper GetDbConnection()
        {
            return new DatabaseWrapper(new NpgsqlConnection(_connectionString));
        }
    }
}