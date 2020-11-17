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
    public interface IRosterRepository
    {
        public Task<IEnumerable<Roster>> GetAll();
        public Task AddPlayer(int rosterId, int playerId);
        public Task RemovePlayer(int rosterId, int playerId);
        public Task Create(Roster roster);
        public Task Update(int id, Roster roster);
        public Task Delete(int id);
        public Task SeedDefault();
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
                .Select(r => new Roster 
                {
                    Id = r.Id,
                    Team = r.Team,
                    Players = r.Players.OrderBy(p => p.Position).ToList()
                })
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

        public async Task Create(Roster roster)
        {
            _context.Rosters.Add(roster);

            await _context.Database.OpenConnectionAsync();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Rosters ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Rosters OFF");
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }

        public async Task Update(int id, Roster roster)
        {
            _context.Entry(roster).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);

            _context.Rosters.Remove(roster);

            await _context.SaveChangesAsync();
        }

        public async Task SeedDefault()
        {
            string sql = @"INSERT INTO Rosters (Team)
                           VALUES ('Default')";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
