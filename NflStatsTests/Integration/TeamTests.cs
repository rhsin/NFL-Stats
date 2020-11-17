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
    public class TeamTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 1;

        public TeamTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTeams()
        {
            var response = await _client.GetAsync("api/Teams");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(32, teams.Count());
            Assert.All(teams, t => Assert.NotEmpty(t.Players));
            Assert.Contains("Los Angeles Chargers", stringResponse);
            Assert.Contains("AFC West", stringResponse);
            Assert.Contains("Philip Rivers", stringResponse);
        }

        [Fact]
        public async Task GetTeam()
        {
            var response = await _client.GetAsync($"api/Teams/{_id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<Team>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Arizona Cardinals", team.Name);
            Assert.Equal("ARI", team.Alias);
            Assert.Equal("NFC", team.Conference);
            Assert.Equal("NFC West", team.Division);
            Assert.Equal(49, team.Players.Count());
            Assert.Contains("Kyler Murray", stringResponse);
        }

        [Fact]
        public async Task PutTeam()
        {
            var team = new Team 
            { 
                Id = 1,
                Name = "Arizona Cardinals",
                Alias = "ARI",
                Conference = "NFC",
                Division = "NFC West" 
            };
            var json = JsonConvert.SerializeObject(team);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/Teams/1", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SeedTeams()
        {
            var response = await _client.PostAsync("api/Seeders/Run/Teams", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Teams Already Seeded!", stringResponse);
        }

        //[Fact]
        //public async Task RefreshTeams()
        //{
        //    var response = await _client.PostAsync("api/Seeders/Refresh/Teams", null);
        //    var stringResponse = await response.Content.ReadAsStringAsync();

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    Assert.Equal("Teams Refreshed Successfully!", stringResponse);
        //}
    }
}
