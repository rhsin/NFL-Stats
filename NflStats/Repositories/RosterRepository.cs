using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NflStats.Data;
using NflStats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NflStats.Repositories
{
    public interface IRosterRepository
    {
        public Task<IEnumerable<Roster>> GetAll();
        public Task AddPlayer(int rosterId, int playerId);
        public Task RemovePlayer(int rosterId, int playerId);
    }

    public class RosterRepository : IRosterRepository
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;

        public RosterRepository(ApplicationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IEnumerable<Roster>> GetAll()
        {
            return await _context.Rosters
                .Include(r => r.Players)
                .ToListAsync();
        }

        public async Task AddPlayer(int rosterId, int playerId)
        {
            var parameters = new { RosterId = rosterId, PlayerId = playerId };

            string sql = @"UPDATE Players
                           SET RosterId = @RosterId
                           WHERE Id = @PlayerId";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task RemovePlayer(int rosterId, int playerId)
        {
            var parameters = new { RosterId = rosterId, PlayerId = playerId };

            string sql = @"UPDATE Players
                           SET RosterId = NULL
                           WHERE Id = @PlayerId";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
