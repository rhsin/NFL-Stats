using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NflStats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NflStats.Repositories
{
    public interface IPlayerRepository
    {
        public Task<IEnumerable<Player>> GetPlayers();
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IConfiguration _config;

        public PlayerRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            string sql = @"SELECT TOP 100 *
                           FROM Players";

            return await this.ExecutePlayerQuery(sql, null);
        }

        private async Task<IEnumerable<Player>> ExecutePlayerQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var players = await connection.QueryAsync<Player>(sql, parameters);

                return players;
            }
        }
    }
}
