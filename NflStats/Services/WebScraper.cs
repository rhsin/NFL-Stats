using AngleSharp;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NflStats.Services
{
    public interface IWebScraper
    {
        public Task<IEnumerable<Player>> GetPlayers();
    }

    public class WebScraper : IWebScraper
    {
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(
                @"https://www.footballdb.com/fantasy-football/index.html?pos=QB&yr=2020&wk=8&rules=1");
            var elements = document.QuerySelectorAll("span.hidden-xs");

            return elements.Select(e => new Player 
            {
                Name = e.TextContent
            })
            .ToList();

            //var players = new List<Player>();

            //foreach (var span in elements)
            //{
            //    players.Add(new Player
            //    {
            //        Name = span.TextContent
            //    });
            //}

            //return players.ToList();
        }
    }
}
