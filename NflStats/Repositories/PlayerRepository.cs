using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NflStats.Repositories
{
    public interface IPlayerRepository
    {
        public Task<IEnumerable<Player>> GetAll();
        public Task<IEnumerable<Player>> GetTop();
        public Task<IEnumerable<Player>> FindBy(string position, string name);
        public Task<IEnumerable<Player>> FindByStats(string field,string type, int value);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IConfiguration _config;
        private readonly SQLValidator _sqlValidator;

        public PlayerRepository(IConfiguration config)
        {
            _config = config;
            _sqlValidator = new SQLValidator();
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

        public async Task<IEnumerable<Player>> FindBy(string position, string name)
        {
            var parameters = new { Position = $"%{position}%", Name = $"%{name}%" };

            string sql = @"SELECT *
                           FROM Players
                           WHERE LOWER(Position) LIKE LOWER(@Position)
                           AND LOWER(Name) LIKE LOWER(@Name)
                           ORDER BY Points DESC";

            return await this.ExecutePlayerQuery(sql, parameters);
        }

        public async Task<IEnumerable<Player>> FindByStats(string field, string type, int value)
        {
            string column = _sqlValidator.Column(field, type);

            var parameters = new { Value = value };

            string sql = @$"SELECT *
                            FROM Players
                            WHERE {column} >= @Value
                            ORDER BY {column} DESC";

            return await this.ExecutePlayerQuery(sql, parameters);
        }

        private async Task<IEnumerable<Player>> ExecutePlayerQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var players = await connection.QueryAsync<Player>(sql, parameters);

                return players;
            }
        }
    }
}
