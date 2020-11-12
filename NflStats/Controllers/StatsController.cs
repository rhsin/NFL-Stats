using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IFantasyPoints _fantasyPoints;
        private readonly IStatsCalculator _statsCalculator;

        public StatsController(IFantasyPoints fantasyPoints, IStatsCalculator statsCalculator)
        {
            _fantasyPoints = fantasyPoints;
            _statsCalculator = statsCalculator;
        }

        // GET: api/Stats/Fantasy/412/8
        // Finds Player from WebScraper with matching name and returns updated weekly points.
        [HttpGet("Fantasy/{id}/{week}")]
        public async Task<ActionResult<Player>> GetFantasyPlayer(int id, int week)
        {
            var player = await _fantasyPoints.GetPlayer(id, week);

            return Ok(player);
        }

        // GET: api/Stats/Fantasy/Rosters/1/8
        // Filters Players from WebScraper that have matching name in selected Roster, and returns
        // updated weekly points.
        [HttpGet("Fantasy/Rosters/{id}/{week}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetFantasyRoster(int id, int week)
        {
            var players = await _fantasyPoints.GetRoster(id, week);

            return Ok(players);
        }

        // POST: api/Stats/Ratio/Passing
        // Calculates each player's TD/Turnover (Int + Fumbles) Ratio and returns 
        // Player list ordered by the ratio, set as the Points field.
        [HttpPost("Ratio/Passing")]
        public ActionResult<IEnumerable<Player>> GetTDRatio(List<Player> players)
        {
            return Ok(_statsCalculator.TDRatio(players));
        }
    }
}
