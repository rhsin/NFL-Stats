using Microsoft.EntityFrameworkCore;
using NflStats.Data;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Services
{
    public interface IFantasyPoints
    {
        public Task<Player> GetPlayer(int id, int week);
        public Task<IEnumerable<Player>> GetRoster(int id, int week);
    }

    public class FantasyPoints : IFantasyPoints
    {
        private readonly ApplicationContext _context;
        private readonly IWebScraper _webScraper;

        public FantasyPoints(ApplicationContext context, IWebScraper webScraper)
        {
            _context = context;
            _webScraper = webScraper;
        }
        
        // Returns Player(Id) with updated points from passed week, fetched using WebScraper.
        public async Task<Player> GetPlayer(int id, int week)
        {
            var players = await _webScraper.GetPlayers(week, "QB%2CRB%2CWR%2CTE");
            var player = await _context.Players.FindAsync(id);

            player.Points = players.Where(p => p.Name == player.Name).First().Points;

            return player;
        }

        // Returns list of Players from Roster(Id) with updated points from passed week, fetched using WebScraper.
        public async Task<IEnumerable<Player>> GetRoster(int id, int week)
        {
            var players = await _webScraper.GetPlayers(week, "QB%2CRB%2CWR%2CTE");

            var roster = await _context.Rosters
                .Where(r => r.Id == id)
                .Select(r => r.Players)
                .SingleAsync();

            var rosterPlayers = roster
                .Where(r => players.Any(p => p.Name == r.Name))
                .ToList();

            rosterPlayers.ForEach(rp =>
                rp.Points = players.Where(p => p.Name == rp.Name).First().Points);

            return rosterPlayers;
        }
    }
}
