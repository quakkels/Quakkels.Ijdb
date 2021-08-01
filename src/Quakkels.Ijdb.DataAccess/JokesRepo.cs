using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Quakkels.Ijdb.Domain.Jokes;

namespace Quakkels.Ijdb.DataAccess
{
    public class JokesRepo : IJokesRepo
    {
        private readonly IDbConnectionProvider _connectionProvider;
        
        public JokesRepo(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<IJoke>> GetJokes()
        {
            const string sql = "SELECT id, line_1 as line1, created_at as createdAt, approved_at as approvedAt FROM Jokes;";
            using var conn = _connectionProvider.GetDbConnection();
            return await conn.QueryAsync<Joke>(sql);
        }

        public async Task<int> InsertJoke(string joke)
        {
            const string sql = "INSERT INTO public.jokes (line_1) VALUES (:joke) RETURNING id;";
            var parameters = new DynamicParameters();
            parameters.Add("joke", joke, DbType.AnsiString);
            using var conn = _connectionProvider.GetDbConnection();
            return await conn.ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<IJoke> GetJokeById(int id)
        {
            const string sql = @"
              SELECT id, line_1 as line1, created_at as createdAt, approved_at as approvedAt 
              FROM Jokes
              WHERE id = :id
              LIMIT 1;";
            
            var param = new DynamicParameters();
            param.Add(":id", id);
            using var conn = _connectionProvider.GetDbConnection();
            return await conn.QueryFirstAsync<Joke>(sql, param);
        }
    }
}