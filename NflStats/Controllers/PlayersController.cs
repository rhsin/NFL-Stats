﻿using Microsoft.AspNetCore.Mvc;
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
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IPlayerRepository _playerRepository;
        private readonly IFantasyPoints _fantasyPoints;

        public PlayersController(ApplicationContext context, IPlayerRepository playerRepository,
            IFantasyPoints fantasyPoints)
        {
            _context = context;
            _playerRepository = playerRepository;
            _fantasyPoints = fantasyPoints;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return Ok(await _playerRepository.GetTop());
        }

        // GET: api/Players/Find
        // Finds Players with position/name values that contain the passed parameters using SQL LIKE operator.
        [HttpGet("Find")]
        public async Task<ActionResult<IEnumerable<Player>>> FindPlayer(string position, string name)
        {
            try
            {
                return Ok(await _playerRepository.FindBy(position, name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Players/Stats
        // Finds Players by stats-field (Yards) & input-type (passing) that are greater than the passed value.
        [HttpGet("Stats")]
        public async Task<ActionResult<IEnumerable<Player>>> FindByStats(string field, string type, int value)
        {
            try
            {
                return Ok(await _playerRepository.FindByStats(field, type, value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var players = await _playerRepository.GetAll();

            try
            {
                var player = players.First(p => p.Id == id);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Players/Fantasy/412/8
        // Finds Player from WebScraper with matching name and returns updated weekly points.
        [HttpGet("Fantasy/{id}/{week}")]
        public async Task<ActionResult<Player>> GetFantasyPlayer(int id, int week)
        {
            try
            {
                var player = await _fantasyPoints.GetPlayer(id, week);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Players/Fantasy/Rosters/1/8
        // Filters Players from WebScraper that have matching name in selected Roster, and returns
        // updated weekly points.
        [HttpGet("Fantasy/Rosters/{id}/{week}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetFantasyRoster(int id, int week)
        {
            try
            {
                var players = await _fantasyPoints.GetRoster(id, week);

                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
