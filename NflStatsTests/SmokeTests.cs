using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NflStatsTests
{
    public class SmokeTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;

        public SmokeTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("api/Players")]
        [InlineData("api/Players/1827")]
        [InlineData("api/Players/Find?position=QB")]
        [InlineData("api/Players/Stats?field=yards&type=QB&value=4900")]
        [InlineData("api/Players/Fantasy/1827/8")]
        [InlineData("api/Rosters")]
        [InlineData("api/Rosters/1")]
        [InlineData("api/Rosters/Check/1")]
        [InlineData("api/Players/Fantasy/Rosters/1/8")]
        public async Task TestGetEndpoints(string url)
        {
            var response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/Seeders/Run/Players")]
        //[InlineData("api/Seeders/Refresh/Players")]
        public async Task TestPostEndpoints(string url)
        {
            var response = await _client.PostAsync(url, null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("api/Rosters/Players/Add/1/1806")]
        [InlineData("api/Rosters/Players/Remove/1/1806")]
        public async Task TestPutEndpoints(string url)
        {
            var response = await _client.PutAsync(url, null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
