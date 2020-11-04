using AngleSharp;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Services
{
    public interface IWebScraper
    {
        public Task<IEnumerable<Player>> GetPlayers(string position);
    }

    public class WebScraper : IWebScraper
    {
        private readonly IBrowsingContext _context;

        public WebScraper()
        {
            _context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        }

        public async Task<IEnumerable<Player>> GetPlayers(string position = "QB")
        {
            var document = await _context.OpenAsync(
                $@"https://www.footballdb.com/fantasy-football/index.html?pos={position}&yr=2020&wk=8&rules=1");
            var elements = document.QuerySelectorAll("tr.right");

            var players = new List<Player>();

            foreach (var e in elements)
            {
                float points;
                float.TryParse(e.QuerySelector("td.hilite")?.TextContent, out points);

                players.Add(new Player
                {
                    Name = e.QuerySelector("a")?.TextContent,
                    Points = points
                });
            }

            return players;
        }
    }
}
