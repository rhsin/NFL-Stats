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
    public class ErrorTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;

        public ErrorTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ErrorResponse()
        {
            var response = await _client.GetAsync("api/Rosters/1000");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(stringResponse);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal(500, error.StatusCode);
            Assert.Equal("Sequence contains no matching element", error.Message);
            Assert.Contains("RostersController.GetRoster", error.StackTrace);
        }

        [Fact]
        public async Task FindByStatsError()
        {
            var response = await _client.GetAsync("api/Players/Stats?field=yard&type=QB&value=4900");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(stringResponse);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("Invalid Type Parameter!", error.Message);
        }

        [Fact]
        public async Task CheckFantasyError()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", Points = 8 },
                new Player { Position = "QB", Points = 2 }
            };
            var json = JsonConvert.SerializeObject(players);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Rosters/Fantasy", data);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<Error>(stringResponse);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("Fantasy Lineup Not Valid!", error.Message);
        }

        [Fact]
        public async Task AddPlayerError()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Add/1/a", null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RemovePlayerError()
        {
            var response = await _client.PutAsync($"api/Rosters/Players/Remove/1/a", null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutRosterError()
        {
            var roster = new Roster { Id = 1000, Team = "Team Ryan" };
            var json = JsonConvert.SerializeObject(roster);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/Rosters/1", data);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutTeamError()
        {
            var team = new Team { Id = 1000, Conference = "NFC" };
            var json = JsonConvert.SerializeObject(team);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/Teams/1", data);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
