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
        public Task<IDictionary<string, Player>> GetTeamLeaders(string team, int season);
        public Task<IEnumerable<object>> FindTeamLeaders(string team, int season);
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
                .Where(p => p.Season == season)
                .Where(p => p.Points > 50)
                .OrderByDescending(p => p.Points)
                .ToList();

            return teamStat;
        }

        public async Task<IDictionary<string, Player>> GetTeamLeaders(string team, int season)
        {
            var teamStat = await _context.TeamStats                
                .Where(ts => ts.TeamName.Contains(team))
                .Where(ts => ts.Season == season)
                .FirstAsync(); 

            var players = await _context.Players
                .Where(p => p.TeamId == teamStat.TeamId)
                .Where(p => p.Season == season)
                .ToListAsync();

            foreach (var p in players)
            {
                var ratio = (float)((p.PassYds + p.RushYds + p.RecYds) / teamStat.TotalYds);

                p.Points = ratio;
            }

            return players
                .Where(p => p.Position != "0")
                .OrderByDescending(p => p.Points)
                .GroupBy(p => p.Position)
                .ToDictionary(g => g.Key,
                    g => g.Where(p => p.Points == g.Max(p => p.Points)).Single());

            //return players
            //    .Where(p => p.Position != "0")
            //    .OrderByDescending(p => p.Points)
            //    .GroupBy(p => p.Position)
            //    .Select(g => new 
            //    { 
            //        Position = g.Key,
            //        Player = g.Where(p => p.Points == g.Max(p => p.Points)).Single()
            //    });
        }

        public async Task<IEnumerable<object>> FindTeamLeaders(string team, int season)
        {
            var parameters = new { Team = $"%{team}%", Season = season };

            string sql = @"SELECT *
                           FROM TeamStats AS ts
                           INNER JOIN Players AS p
                           ON ts.TeamId = p.TeamId
                           WHERE LOWER(ts.TeamName) LIKE LOWER(@Team)
                           AND ts.Season = @Season
                           AND p.Season = @Season
                           AND p.Points > 80
                           ORDER BY p.Points DESC";

            return await this.ExecuteQuery(sql, parameters);
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

        private async Task<IEnumerable<object>> ExecuteQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var result = await connection.QueryAsync<object>(sql, parameters);

                return result;
            }
        }
    }
}
