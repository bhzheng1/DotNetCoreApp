using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Second.DataAccess
{
    public class RepositoryContext
    {
        private readonly string _connectionString;
        private readonly IDbConnection _connection;
        public RepositoryContext(ApplicationDbContext context, IConfiguration config)
        {
            _connection = context.Database.GetDbConnection();
            _connectionString = config.GetConnectionString("ConnectionStrings");
        }

        public async Task<CommandDefinition> GetCommandDefinition(string sql, object parameters = null,
            CommandType? commandType = CommandType.Text)
        {
            return await Task.FromResult(new CommandDefinition(sql, parameters, null, null, commandType));
        }
        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(CommandDefinition cd)
        {
            var connection = _connection ?? new SqlConnection(_connectionString);
            try
            {
                if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken) connection.Open();
                var result = await connection.QueryAsync<TResult>(cd);
                return result;
            }
            catch (Exception)
            {
                return await Task.FromResult(default(IEnumerable<TResult>));
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
