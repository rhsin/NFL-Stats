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
        public Task<IEnumerable<Player>> GetAll();
        public Task<IEnumerable<Player>> GetTop();
        public Task<IEnumerable<Player>> GetPosition(string position);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IConfiguration _config;

        public PlayerRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            string sql = @"SELECT *
                           FROM Players";

            return await this.ExecutePlayerQuery(sql, null);
        }

        public async Task<IEnumerable<Player>> GetTop()
        {
            string sql = @"SELECT TOP 100 *
                           FROM Players
                           ORDER BY Points DESC";

            return await this.ExecutePlayerQuery(sql, null);
        }

        public async Task<IEnumerable<Player>> GetPosition(string position)
        {
            var parameters = new { Position = position };

            string sql = @"SELECT *
                           FROM Players
                           WHERE UPPER(Position) = UPPER(@Position)
                           ORDER BY Points DESC";

            return await this.ExecutePlayerQuery(sql, parameters);
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
