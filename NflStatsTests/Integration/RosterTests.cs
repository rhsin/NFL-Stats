using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NflStats.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Text;

namespace NflStatsTests.Integration
{
    public class RosterTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 100000;

        public RosterTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetRosters()
        {
            var response = await _client.GetAsync("api/Rosters");
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("team", stringResponse);
            Assert.Contains("players", stringResponse);
            Assert.Contains("Team Ryan", stringResponse);
        }

        [Fact]
        public async Task GetRoster()
        {
            var response = await _client.GetAsync("api/Rosters/1");
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("team", stringResponse);
            Assert.Contains("players", stringResponse);
            Assert.Contains("Team Ryan", stringResponse);
        }

        [Fact]
        public async Task AddPlayer()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Add/1/{_id}", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"Player {_id} Added To Roster 1!", stringResponse);
        }

        [Fact]
        public async Task RemovePlayer()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Remove/1/{_id}", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"Player {_id} Removed From Roster 1!", stringResponse);
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
        }

        [Fact]
        public async Task PutRoster()
        {
            var roster = new Roster { Id = 1, Team = "Team Ryan" };
            var json = JsonConvert.SerializeObject(roster);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/Rosters/1", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PostRoster()
        {
            var roster = new Roster { Id = 100, Team = "Team Test" };
            var json = JsonConvert.SerializeObject(roster);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Rosters", data);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains("Team Test", stringResponse);
        }

        [Fact]
        public async Task DeleteRoster()
        {
            var response = await _client.DeleteAsync("api/Rosters/100");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SeedRosters()
        {
            var response = await _client.PostAsync("api/Seeders/Run/Rosters", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Rosters Already Seeded!", stringResponse);
        }
    }
}
