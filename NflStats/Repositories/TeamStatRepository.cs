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
        public Task<TeamStat> FindBy(string team, int season);
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
            var teamStats = await _context.TeamStats
                .Include(ts => ts.Team)
                .ThenInclude(t => t.Players)
                .ToListAsync();

            foreach (var ts in teamStats)
            {
                ts.Team.Players = ts.Team.Players
                    .Where(p => p.Points > 100)
                    .OrderByDescending(p => p.Points)
                    .ToList();
            }

            return teamStats;
        }

        public async Task<TeamStat> FindBy(string team, int season)
        {
            var teamStat = await _context.TeamStats
                .Include(ts => ts.Team)
                .ThenInclude(t => t.Players)
                .Where(ts => ts.TeamName.Contains(team))
                .Where(ts => ts.Season == season)
                .FirstAsync();

            teamStat.Team.Players = teamStat.Team.Players
                .Where(p => p.Points > 100)
                .OrderByDescending(p => p.Points)
                .ToList();

            return teamStat;
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
