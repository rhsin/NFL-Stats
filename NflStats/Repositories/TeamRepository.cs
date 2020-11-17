using Microsoft.EntityFrameworkCore;
using NflStats.Data;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Repositories
{
    public interface ITeamRepository
    {
        public Task<IEnumerable<Team>> GetAll();
        public Task<IEnumerable<Team>> FindByDivision(string division);
        public Task Update(int id, Team team);
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationContext _context;

        public TeamRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _context.Teams
                .Include(t => t.Players)
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                    Alias = t.Alias,
                    Conference = t.Conference,
                    Division = t.Division,
                    Players = t.Players
                        .Where(p => p.Points > 100)
                        .OrderBy(p => p.Position)
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> FindByDivision(string division)
        {
            var teams = await _context.Teams
                .Include(t => t.Players)
                .Where(t => t.Division == division)
                .ToListAsync();

            foreach (var t in teams)
            {
                t.Players = t.Players
                    .Where(p => p.Points > 150)
                    .OrderByDescending(p => p.Points)
                    .ToList();      
            }

            return teams;
        }

        public async Task Update(int id, Team team)
        {
            _context.Entry(team).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
