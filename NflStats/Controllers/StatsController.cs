using Microsoft.AspNetCore.Mvc;
using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
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
            return Ok(await _fantasyPoints.GetPlayer(id, week));
        }

        // GET: api/Stats/Fantasy/Rosters/1/8
        // Filters Players from WebScraper that have matching name in selected Roster, and returns
        // updated weekly points.
        [HttpGet("Fantasy/Rosters/{id}/{week}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetFantasyRoster(int id, int week)
        {
            return Ok(await _fantasyPoints.GetRoster(id, week));
        }

        // POST: api/Stats/Touchdowns
        // Calculates each players total touchdowns and returns Player 
        // list ordered by the total, set as the Points field.
        [HttpPost("Touchdowns")]
        public ActionResult<IEnumerable<Player>> GetTouchdowns(List<Player> players)
        {
            return Ok(_statsCalculator.TotalTDs(players));
        }

        // POST: api/Stats/Ratio
        // Calculates each players(QB) TD/Turnover Ratio and returns Player 
        // list ordered by the ratio, set as the Points field, if between 0, 100.
        [HttpPost("Ratio")]
        public ActionResult<IEnumerable<Player>> GetTDRatio(List<Player> players)
        {
            return Ok(_statsCalculator.TDRatio(players));
        }

        // POST: api/Scrimmage
        // Calculates each players Yards from Scrimmage (Rush + Rec) and returns 
        // Player list ordered by the YFS, set as the Points field.
        [HttpPost("Scrimmage")]
        public ActionResult<IEnumerable<Player>> GetScrimmageYds(List<Player> players)
        {
            return Ok(_statsCalculator.ScrimmageYds(players));
        }
    }
}
