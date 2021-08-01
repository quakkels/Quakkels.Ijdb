using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Quakkels.Ijdb.DataAccess
{
    public class DatabaseWrapper : IDatabaseWrapper
    {
        private readonly NpgsqlConnection _connection;
        public DatabaseWrapper(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            return await _connection.QueryAsync<T>(sql);
        }

        public async Task<T> QueryFirstAsync<T>(string sql, DynamicParameters param)
        {
            return await _connection.QueryFirstAsync<T>(sql, param);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters param)
        {
            return await _connection.ExecuteScalarAsync<T>(sql, param);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}