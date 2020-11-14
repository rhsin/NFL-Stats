using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NflStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NflStatsTests.Integration
{
    public class StatsTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 1827;

        public StatsTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetFantasyPlayer()
        {
            var response = await _client.GetAsync($"api/Stats/Fantasy/{_id}/8");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Dalvin Cook", player.Name);
            Assert.Equal(46, player.Points);
        }

        [Fact]
        public async Task GetFantasyRoster()
        {
            var response = await _client.GetAsync("api/Stats/Fantasy/Rosters/1/8");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(players.Count() > 0);
            Assert.Contains("Dalvin Cook", stringResponse);
            Assert.Contains("RB", stringResponse);
            Assert.Contains("MIN", stringResponse);
            Assert.Contains("46", stringResponse);
        }

        [Fact]
        public async Task GetTDORatio()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", PassTds = 10, RushTds = 1, RecTds = 1, PassInt = 3, Fumbles = 3, Points = 0 },
                new Player { Position = "QB", PassTds = 24, RushTds = 0, RecTds = 1, PassInt = 6, Fumbles = 4, Points = 0 },
                new Player { Position = "QB", PassTds = 12, RushTds = 2, RecTds = 1, PassInt = 10, Fumbles = 5, Points = 0 },
                new Player { Position = "WR", PassTds = 0, RushTds = 1, RecTds = 14, PassInt = 0, Fumbles = 6, Points = 0 }
            };
            var json = JsonConvert.SerializeObject(players);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Stats/Ratio/Passing", data);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var playerStats = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Collection(playerStats,
                item => Assert.Equal(2.5, item.Points),
                item => Assert.Equal(2, item.Points),
                item => Assert.Equal(1, item.Points));
        }

        [Fact]
        public async Task GetScrimmageYds()
        {
            var players = new List<Player>
            {
                new Player { RushYds = 1100, RecYds = 300, Points = 0 },
                new Player { RushYds = 550, RecYds = 1000, Points = 0 },
                new Player { RushYds = 850, RecYds = 900, Points = 0 }
            };
            var json = JsonConvert.SerializeObject(players);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Stats/Scrimmage", data);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var playerStats = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Collection(playerStats,
                item => Assert.Equal(1750, item.Points),
                item => Assert.Equal(1550, item.Points),
                item => Assert.Equal(1400, item.Points));
        }
    }
}
