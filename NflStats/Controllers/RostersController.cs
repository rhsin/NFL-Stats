using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NflStats.Data;
using NflStats.Models;
using NflStats.Repositories;
using NflStats.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RostersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRosterRepository _rosterRepository;
        private readonly ILineupValidator _lineupValidator;

        public RostersController(ApplicationContext context, IRosterRepository rosterRepository,
            ILineupValidator lineupValidator)
        {
            _context = context;
            _rosterRepository = rosterRepository;
            _lineupValidator = lineupValidator;
        }

        // GET: api/Rosters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roster>>> GetRosters()
        {
            return Ok(await _rosterRepository.GetAll());
        }

        // GET: api/Rosters/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Roster>> GetRoster(int id)
        {
            var rosters = await _rosterRepository.GetAll();

            try
            {
                var roster = rosters.First(p => p.Id == id);

                return Ok(roster);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Rosters/Players/Add/1/10
        [HttpPut("Players/Add/{rosterId}/{playerId}")]
        public async Task<IActionResult> AddPlayer(int rosterId, int playerId)
        {
            try
            {
                await _rosterRepository.AddPlayer(rosterId, playerId);

                return Ok($"Player {playerId} Added To Roster {rosterId}!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        // PUT: api/Rosters/Players/Remove/1/10
        [HttpPut("Players/Remove/{rosterId}/{playerId}")]
        public async Task<IActionResult> RemovePlayer(int playerId, int rosterId = 1)
        {
            try
            {
                await _rosterRepository.RemovePlayer(rosterId, playerId);

                return Ok($"Player {playerId} Removed From Roster {rosterId}!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Rosters/Check/1
        // Checks if Players in Roster make a standard fantasy team lineup.
        [HttpGet("Check/{id}")]
        public async Task<ActionResult<Roster>> CheckRoster(int id)
        {
            var rosters = await _rosterRepository.GetAll();

            try
            {
                var roster = rosters.First(p => p.Id == id);

                if (!_lineupValidator.Standard(roster))
                {
                    return Ok($"Roster {roster.Id} Is Not Valid Lineup!");
                }

                return Ok(roster);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Rosters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoster(int id, Roster roster)
        {
            if (id != roster.Id)
            {
                return BadRequest();
            }

            _context.Entry(roster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RosterExists(id))
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

        // POST: api/Rosters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Roster>> PostRoster(Roster roster)
        {
            _context.Rosters.Add(roster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoster", new { id = roster.Id }, roster);
        }

        // DELETE: api/Rosters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Roster>> DeleteRoster(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);
            if (roster == null)
            {
                return NotFound();
            }

            _context.Rosters.Remove(roster);
            await _context.SaveChangesAsync();

            return roster;
        }

        private bool RosterExists(int id)
        {
            return _context.Rosters.Any(e => e.Id == id);
        }
    }
}
