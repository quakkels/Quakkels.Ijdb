using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace Quakkels.Ijdb.DataAccess
{
    public interface IDatabaseWrapper : IDisposable
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql);
        Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters param);
        Task<T> QueryFirstAsync<T>(string sql, DynamicParameters param);
    }
}