using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NflStats.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NflStatsTests.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetPlayers()
        {
            var response = await _client.GetAsync("api/Players");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(100, players.Count());
            Assert.Contains("Christian McCaffrey", stringResponse);
            Assert.Contains("RB", stringResponse);
            Assert.Contains("CAR", stringResponse);
            Assert.Contains("469.2", stringResponse);
        }

        [Fact]
        public async Task GetPlayer()
        {
            var response = await _client.GetAsync("api/Players/408");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Lamar Jackson", player.Name);
            Assert.Equal("QB", player.Position);
            Assert.Equal("BAL", player.Team);
            Assert.Equal(415.68, Math.Round(player.Points, 2));
        }

        [Fact]
        public async Task GetPosition()
        {
            var response = await _client.GetAsync("api/Players/Position/QB");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(71, players.Count());
            Assert.All(players, p => Assert.Equal("QB", p.Position));
            Assert.Contains("Dak Prescott", stringResponse);
            Assert.Contains("Jarrett Stidham", stringResponse);
        }

        [Fact]
        public async Task GetRosters()
        {
            var response = await _client.GetAsync("api/Rosters");
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("team", stringResponse);
            Assert.Contains("players", stringResponse);
        }

        [Fact]
        public async Task GetRoster()
        {
            var response = await _client.GetAsync("api/Rosters/1");
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("team", stringResponse);
            Assert.Contains("players", stringResponse);
        }

        [Fact]
        public async Task SeedPlayers()
        {
            var response = await _client.PostAsync("api/Seeders/Run/Players", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Players Already Seeded!", stringResponse);
        }
    }
}
