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
        public Task<IEnumerable<Roster>> GetTop();
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

        public async Task<IEnumerable<Roster>> GetTop()
        {
            string sql = @"SELECT TOP 100 *
                           FROM Rosters";

            return await this.ExecuteRosterQuery(sql, null);
        }

        private async Task<IEnumerable<Roster>> ExecuteRosterQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var rosters = await connection.QueryAsync<Roster>(sql, parameters);

                return rosters;
            }
        }
    }
}
