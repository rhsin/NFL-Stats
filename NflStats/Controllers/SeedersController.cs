using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NflStats.Data;
using NflStats.Models;
using NflStats.Services;

namespace NflStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ICsvImporter _csvImporter;

        public SeedersController(ApplicationContext context, ICsvImporter csvImporter)
        {
            _context = context;
            _csvImporter = csvImporter;
        }

        // GET: api/Seeders/Csv/Players
        [HttpGet("Csv/Players")]
        public List<Player> GetPlayerRecords()
        {
            return _csvImporter.GetPlayerRecords();
        }

        // POST: api/Seeders/Run/Players
        [HttpPost("Run/Players")]
        public IActionResult SeedPlayers()
        {
            if (_context.Players.Any())
            {
                return BadRequest("Players Already Seeded!");
            }

            try
            {
                var players = _csvImporter.GetPlayerRecords();

                _context.Players.AddRange(players);
                _context.SaveChanges();

                return Ok("Players Seeded Successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
