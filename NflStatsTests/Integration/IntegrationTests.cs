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
using System.Text;

namespace NflStatsTests.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 1827;
        private readonly int _pId = 1806;

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
            var response = await _client.GetAsync($"api/Players/{_id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Dalvin Cook", player.Name);
            Assert.Equal("RB", player.Position);
            Assert.Equal("MIN", player.Team);
            Assert.Equal(292.4, Math.Round(player.Points, 2));
        }

        [Fact]
        public async Task FindPlayerPosition()
        {
            var response = await _client.GetAsync("api/Players/Find?position=QB");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(71, players.Count());
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
            Assert.Single(players);
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
            Assert.Equal(2, players.Count());
            Assert.All(players, p => Assert.True(p.PassYds > 4900));
            Assert.Contains("Jameis Winston", stringResponse);
            Assert.Contains("Dak Prescott", stringResponse);
        }

        [Fact]
        public async Task GetFantasyPlayer()
        {
            var response = await _client.GetAsync($"api/Players/Fantasy/{_id}/8");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Dalvin Cook", player.Name);
            Assert.Equal(46, player.Points);
        }

        [Fact]
        public async Task GetFantasyRoster()
        {
            var response = await _client.GetAsync("api/Players/Fantasy/Rosters/1/8");
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
        public async Task CheckFantasy()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", Points = 8 },
                new Player { Position = "RB", Points = 2 }
            };
            var json = JsonConvert.SerializeObject(players);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Rosters/Fantasy", data);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var points = JsonConvert.DeserializeObject<float>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(10, points);
        }

        [Fact]
        public async Task CheckFantasyRoster()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", Points = 8 },
                new Player { Position = "RB", Points = 2 }
            };
            var json = JsonConvert.SerializeObject(players);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Rosters/Fantasy/1", data);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var points = JsonConvert.DeserializeObject<float>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(points > 200);

            //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddPlayer()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Add/1/{_pId}", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"Player {_pId} Added To Roster 1!", stringResponse);
        }

        [Fact]
        public async Task RemovePlayer()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Remove/1/{_pId}", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"Player {_pId} Removed From Roster 1!", stringResponse);
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
