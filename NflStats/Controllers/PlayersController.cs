using Microsoft.AspNetCore.Mvc;
using NflStats.Models;
using NflStats.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return Ok(await _playerRepository.GetTop());
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var players = await _playerRepository.GetAll();
            var player = players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // GET: api/Players/Season/2019
        [HttpGet("Season/{season}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetSeason(int season)
        {
            return Ok(await _playerRepository.GetSeason(season));
        }

        // GET: api/Players/Find
        // Finds Players with position/name values that contain the passed parameters using SQL LIKE operator.
        [HttpGet("Find")]
        public async Task<ActionResult<IEnumerable<Player>>> FindPlayer(string position, string name)
        {
            return Ok(await _playerRepository.FindBy(position, name));
        }

        // GET: api/Players/Stats
        // Finds Players by stats-field (Yards) & input-type (passing) that are greater than the passed value.
        [HttpGet("Stats")]
        public async Task<ActionResult<IEnumerable<Player>>> FindByStats(string field, string type, int value)
        {
            return Ok(await _playerRepository.FindByStats(field, type, value));
        }

        // GET: api/Players/Team
        // Finds Players with teamId/season values that contain the passed parameters using SQL query.
        [HttpGet("Team")]
        public async Task<ActionResult<IEnumerable<Player>>> FindByTeam(int teamId, int season)
        {
            return Ok(await _playerRepository.FindByTeam(teamId, season));
        }
    }
}
