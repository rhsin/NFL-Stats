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
    public class TeamStatsController : ControllerBase
    {
        private readonly ITeamStatRepository _teamStatRepository;

        public TeamStatsController(ITeamStatRepository teamStatRepository)
        {
            _teamStatRepository = teamStatRepository;
        }

        // GET: api/TeamStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamStat>>> GetTeamStats()
        {
            return Ok(await _teamStatRepository.GetAll());
        }

        // GET: api/TeamStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamStat>> GetTeamStat(int id)
        {
            var teamStats = await _teamStatRepository.GetAll();
            var teamStat = teamStats.First(ts => ts.Id == id);

            return Ok(teamStat);
        }
    }
}
