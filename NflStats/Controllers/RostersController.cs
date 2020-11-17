using Microsoft.AspNetCore.Mvc;
using NflStats.Models;
using NflStats.Repositories;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RostersController : ControllerBase
    {
        private readonly IRosterRepository _rosterRepository;
        private readonly ILineupValidator _lineupValidator;

        public RostersController(IRosterRepository rosterRepository, ILineupValidator lineupValidator)
        {
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
            var roster = rosters.FirstOrDefault(r => r.Id == id);

            if (roster == null)
            {
                return NotFound();
            }

            return roster;
        }

        // PUT: api/Rosters/Players/Add/1/10
        [HttpPut("Players/Add/{rosterId}/{playerId}")]
        public async Task<IActionResult> AddPlayer(int rosterId, int playerId)
        {
            await _rosterRepository.AddPlayer(rosterId, playerId);

            return Ok($"Player {playerId} Added To Roster {rosterId}!");  
        }

        // PUT: api/Rosters/Players/Remove/1/10
        [HttpPut("Players/Remove/{rosterId}/{playerId}")]
        public async Task<IActionResult> RemovePlayer(int playerId, int rosterId = 1)
        {
            await _rosterRepository.RemovePlayer(rosterId, playerId);

            return Ok($"Player {playerId} Removed From Roster {rosterId}!");
        }

        // POST: api/Rosters/Fantasy/1
        // Checks if Players list is standard fantasy team lineup, and returns total
        // fantasy points if valid. If Roster Id parameter is passed, the Players
        // list from that Roster is checked.
        [HttpPost("Fantasy/{id?}")]
        public async Task<ActionResult<float>> CheckFantasy(int? id, List<Player> players)
        {
            if (id.HasValue)
            {
                var rosters = await _rosterRepository.GetAll();
                var roster = rosters.First(r => r.Id == id);
                var rosterPlayers = roster.Players.ToList();

                return Ok(_lineupValidator.TotalPoints(rosterPlayers));
            }

            return Ok(_lineupValidator.TotalPoints(players)); 
        }

        // PUT: api/Rosters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoster(int id, Roster roster)
        {
            if (id != roster.Id)
            {
                return BadRequest();
            }

            await _rosterRepository.Update(id, roster);

            return NoContent();
        }

        // POST: api/Rosters
        [HttpPost]
        public async Task<ActionResult<Roster>> PostRoster(Roster roster)
        {
            await _rosterRepository.Create(roster);

            return CreatedAtAction("GetRoster", new { id = roster.Id }, roster);
        }

        // DELETE: api/Rosters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoster(int id)
        {
            await _rosterRepository.Delete(id);

            return NoContent();
        }
    }
}
