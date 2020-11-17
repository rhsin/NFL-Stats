using Microsoft.AspNetCore.Mvc;
using NflStats.Data;
using NflStats.Models;
using NflStats.Repositories;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ICsvImporter _csvImporter;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRosterRepository _rosterRepository;
        private readonly ITeamStatRepository _teamStatRepository;

        public SeedersController(ApplicationContext context, ICsvImporter csvImporter,
            IPlayerRepository playerRepository, IRosterRepository rosterRepository,
            ITeamStatRepository teamStatRepository)
        {
            _context = context;
            _csvImporter = csvImporter;
            _playerRepository = playerRepository;
            _rosterRepository = rosterRepository;
            _teamStatRepository = teamStatRepository;
        }

        // GET: api/Seeders/Csv/Players/2019
        [HttpGet("Csv/Players/{year}")]
        public IEnumerable<Player> GetPlayerRecords(int year)
        {
            return _csvImporter.GetPlayerRecords(year);
        }

        // GET: api/Seeders/Csv/TeamStat/2019
        [HttpGet("Csv/TeamStat/{year}")]
        public IEnumerable<TeamStat> GetTeamStatRecords(int year)
        {
            return _csvImporter.GetTeamStatRecords(year);
        }

        // POST: api/Seeders/Run/Players
        // Seeds initial Player entities into Players table using CsvImporter.
        [HttpPost("Run/Players")]
        public IActionResult SeedPlayers()
        {
            if (_context.Players.Any())
            {
                return BadRequest("Players Already Seeded!");
            }

            var players2019 = _csvImporter.GetPlayerRecords(2019);
            var players2018 = _csvImporter.GetPlayerRecords(2018);

            foreach (var p in players2019)
            {
                p.Season = 2019;
            }

            foreach (var p in players2018)
            {
                p.Season = 2018;
            }

            _context.Players.AddRange(players2019);
            _context.Players.AddRange(players2018);
            _context.SaveChanges();

            return Ok("Players Seeded Successfully!");
        }

        // POST: api/Seeders/Run/Teams
        // Seeds initial Team entities into Teams table using CsvImporter.
        [HttpPost("Run/Teams")]
        public IActionResult SeedTeams()
        {
            if (_context.Teams.Any())
            {
                return BadRequest("Teams Already Seeded!");
            }

            var teams = _csvImporter.GetTeamRecords();

            _context.Teams.AddRange(teams);
            _context.SaveChanges();

            return Ok("Teams Seeded Successfully!");
        }

        // POST: api/Seeders/Run/TeamStats
        // Seeds initial TeamStat entities into TeamStats table using CsvImporter.
        [HttpPost("Run/TeamStats")]
        public IActionResult SeedTeamStats()
        {
            if (_context.TeamStats.Any())
            {
                return BadRequest("TeamStats Already Seeded!");
            }

            var teamStats2019 = _csvImporter.GetTeamStatRecords(2019);
            var teamStats2018 = _csvImporter.GetTeamStatRecords(2018);
            var teamStats2017 = _csvImporter.GetTeamStatRecords(2017);

            foreach (var ts in teamStats2019)
            {
                ts.Season = 2019;
            }

            foreach (var ts in teamStats2018)
            {
                ts.Season = 2018;
            }

            foreach (var ts in teamStats2017)
            {
                ts.Season = 2017;
            }

            _context.TeamStats.AddRange(teamStats2019);
            _context.TeamStats.AddRange(teamStats2018);
            _context.TeamStats.AddRange(teamStats2017);
            _context.SaveChanges();

            return Ok("TeamStats Seeded Successfully!");
        }

        // POST: api/Seeders/Run/Roster
        // Seeds initial Roster entities into Rosters table using CsvImporter.
        [HttpPost("Run/Rosters")]
        public IActionResult SeedRosters()
        {
            if (_context.Rosters.Any())
            {
                return BadRequest("Rosters Already Seeded!");
            }

            _rosterRepository.SeedDefault();

            return Ok("Rosters Seeded Successfully!");
        }

        // POST: api/Seeders/Refresh/Players
        // Removes all Player entities & Seeds new Players using CsvImporter.
        [HttpPost("Refresh/Players")]
        public IActionResult RefreshPlayers()
        {
            var players2019 = _csvImporter.GetPlayerRecords(2019);
            var players2018 = _csvImporter.GetPlayerRecords(2018);

            foreach (var p in players2019)
            {
                p.Season = 2019;
            }

            foreach (var p in players2018)
            {
                p.Season = 2018;
            }

            _context.Players.RemoveRange(_context.Players);
            _context.Players.AddRange(players2019);
            _context.Players.AddRange(players2018);
            _context.SaveChanges();

            return Ok("Players Refreshed Successfully!");
        }

        // POST: api/Seeders/Refresh/Teams
        // Removes all Team entities & Seeds new Teams using CsvImporter.
        [HttpPost("Refresh/Teams")]
        public IActionResult RefreshTeams()
        {
            var teams = _csvImporter.GetTeamRecords();

            _context.Teams.RemoveRange(_context.Teams);
            _context.Teams.AddRange(teams);
            _context.SaveChanges();

            return Ok("Teams Refreshed Successfully!");
        }

        // POST: api/Seeders/Add/Players/2018
        // Adds Player entities into Players table using CsvImporter, by year.
        [HttpPost("Add/Players/{year}")]
        public IActionResult AddCsvPlayers(int year)
        {
            var players= _csvImporter.GetPlayerRecords(year);

            foreach (var p in players)
            {
                p.Season = year;
            }

            _context.Players.AddRange(players);
            _context.SaveChanges();

            return Ok($"{year} Players Added Successfully!");
        }

        // POST: api/Seeders/Default/Players
        // Updates all Player entities with default values.
        [HttpPost("Default/Players")]
        public IActionResult SeedPlayersDefault()
        {
            _playerRepository.SeedDefaultTeam();

            return Ok("Player Default Values Updated Successfully!");
        }

        // POST: api/Seeders/Default/TeamStats
        // Updates all TeamStat entities with default values.
        [HttpPost("Default/TeamStats")]
        public IActionResult SeedTeamStatsDefault()
        {
            _teamStatRepository.SeedDefaultTeam();

            return Ok("TeamStat Default Values Updated Successfully!");
        }
    }
}
