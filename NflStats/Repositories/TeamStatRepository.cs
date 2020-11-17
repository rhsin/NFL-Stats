using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NflStats.Data;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Repositories
{
    public interface ITeamStatRepository
    {
        public Task<IEnumerable<TeamStat>> GetAll();
        public Task SeedDefaultTeam();
    }

    public class TeamStatRepository : ITeamStatRepository
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;

        public TeamStatRepository(ApplicationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IEnumerable<TeamStat>> GetAll()
        {
            return await _context.TeamStats
                .Include(ts => ts.Team)
                .ThenInclude(t => t.Players)
                .ToListAsync();
        }

        public async Task SeedDefaultTeam()
        {
            string sql = @"UPDATE TeamStats
                           SET TeamStats.TeamId = t.Id
                           FROM TeamStats AS ts
                             INNER JOIN Teams AS t
                             ON ts.TeamName = t.Name";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
