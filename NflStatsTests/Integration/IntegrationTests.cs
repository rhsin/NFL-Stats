using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NflStats.Models;
using System.Collections.Generic;
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
            Assert.IsType<List<Player>>(players);
            Assert.Contains("Christian McCaffrey", stringResponse);
            Assert.Contains("RB", stringResponse);
            Assert.Contains("CAR", stringResponse);
            Assert.Contains("469.2", stringResponse);
        }

        [Fact]
        public async Task SeedPlayersBadRequest()
        {
            var response = await _client.PostAsync("api/Seeders/Run/Players", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Players Already Seeded!", stringResponse);
        }
    }
}
