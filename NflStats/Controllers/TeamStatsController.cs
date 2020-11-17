using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NflStats.Data;
using NflStats.Models;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamStatsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TeamStatsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TeamStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamStat>>> GetTeamStats()
        {
            return await _context.TeamStats.ToListAsync();
        }

        // GET: api/TeamStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamStat>> GetTeamStat(int id)
        {
            var teamStat = await _context.TeamStats.FindAsync(id);

            if (teamStat == null)
            {
                return NotFound();
            }

            return teamStat;
        }

        // PUT: api/TeamStats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamStat(int id, TeamStat teamStat)
        {
            if (id != teamStat.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamStatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeamStats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeamStat>> PostTeamStat(TeamStat teamStat)
        {
            _context.TeamStats.Add(teamStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamStat", new { id = teamStat.Id }, teamStat);
        }

        // DELETE: api/TeamStats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamStat>> DeleteTeamStat(int id)
        {
            var teamStat = await _context.TeamStats.FindAsync(id);
            if (teamStat == null)
            {
                return NotFound();
            }

            _context.TeamStats.Remove(teamStat);
            await _context.SaveChangesAsync();

            return teamStat;
        }

        private bool TeamStatExists(int id)
        {
            return _context.TeamStats.Any(e => e.Id == id);
        }
    }
}
