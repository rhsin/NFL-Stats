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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return Ok(await _teamRepository.GetAll());
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var teams = await _teamRepository.GetAll();
            var team = teams.First(t => t.Id == id);

            return Ok(team);
        }

        // GET: api/Teams/Find
        [HttpGet("Find")]
        public async Task<ActionResult<IEnumerable<Team>>> FindTeam(string division)
        {
            return Ok(await _teamRepository.FindByDivision(division));
        }

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }
            
            await _teamRepository.Update(id, team);

            return NoContent();
        }
    }
}
