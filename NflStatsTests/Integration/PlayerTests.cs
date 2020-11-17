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
    public class PlayerTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 1827;

        public PlayerTests(WebApplicationFactory<NflStats.Startup> factory)
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
            Assert.Equal(50, players.Count());
            Assert.Contains("Christian McCaffrey", stringResponse);
            Assert.Contains("RB", stringResponse);
            Assert.Contains("CAR", stringResponse);
            Assert.Contains("469.2", stringResponse);
        }

        [Fact]
        public async Task GetPlayer()
        {
            var response = await _client.GetAsync($"api/Players/{_id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Dalvin Cook", player.Name);
            Assert.Equal("RB", player.Position);
            Assert.Equal("MIN", player.TeamName);
            Assert.Equal(292.4, Math.Round(player.Points, 2));
        }

        [Fact]
        public async Task GetSeason()
        {
            var response = await _client.GetAsync("api/Players/Season/2018");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(100, players.Count());
            Assert.Contains("Patrick Mahomes", stringResponse);
            Assert.Contains("QB", stringResponse);
            Assert.Contains("KAN", stringResponse);
            Assert.Contains("415.08", stringResponse);
        }

        [Fact]
        public async Task FindPlayerPosition()
        {
            var response = await _client.GetAsync("api/Players/Find?position=QB");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(217, players.Count());
            Assert.All(players, p => Assert.Equal("QB", p.Position));
            Assert.Contains("Dak Prescott", stringResponse);
            Assert.Contains("Jarrett Stidham", stringResponse);
        }

        [Fact]
        public async Task FindPlayerName()
        {
            var response = await _client.GetAsync("api/Players/Find?position=QB&name=Mahomes");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, players.Count());
            Assert.All(players, p => Assert.Equal("QB", p.Position));
            Assert.Contains("Patrick Mahomes", stringResponse);
        }

        [Fact]
        public async Task FindByStats()
        {
            var response = await _client.GetAsync("api/Players/Stats?field=yards&type=QB&value=4900");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(5, players.Count());
            Assert.All(players, p => Assert.True(p.PassYds > 4900));
            Assert.Contains("Jameis Winston", stringResponse);
            Assert.Contains("Dak Prescott", stringResponse);
        }

        [Fact]
        public async Task FindByTeam()
        {
            var response = await _client.GetAsync("api/Players/Team?teamId=1&season=2019");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(15, players.Count());
            Assert.All(players, p => Assert.Equal(1, p.TeamId));
            Assert.All(players, p => Assert.Equal(2019, p.Season));
            Assert.Contains("Kyler Murray", stringResponse);
            Assert.Contains("Larry Fitzgerald", stringResponse);
        }

        [Fact]
        public async Task SeedPlayers()
        {
            var response = await _client.PostAsync("api/Seeders/Run/Players", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Players Already Seeded!", stringResponse);
        }

        //[Fact]
        //public async Task RefreshPlayers()
        //{
        //    var response = await _client.PostAsync("api/Seeders/Refresh/Players", null);
        //    var stringResponse = await response.Content.ReadAsStringAsync();

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    Assert.Equal("Players Refreshed Successfully!", stringResponse);
        //}
    }
}
