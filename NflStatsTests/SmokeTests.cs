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
        [InlineData("api/Players/Season/2019")]
        [InlineData("api/Players/1827")]
        [InlineData("api/Players/Find?position=QB")]
        [InlineData("api/Players/Stats?field=yards&type=QB&value=4900")]
        [InlineData("api/Players/Team?teamId=1&season=2019")]
        [InlineData("api/Stats/Fantasy/1827/8")]
        [InlineData("api/Rosters")]
        [InlineData("api/Rosters/1")]
        [InlineData("api/Teams")]
        [InlineData("api/Teams/1")]
        [InlineData("api/TeamStats")]
        [InlineData("api/TeamStats/1")]
        [InlineData("api/Teams/Find?division=AFC West")]
        [InlineData("api/Stats/Fantasy/Rosters/1/8")]
        public async Task TestGetEndpoints(string url)
        {
            var response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/Rosters/Players/Add/1/100000")]
        [InlineData("api/Rosters/Players/Remove/1/100000")]
        public async Task TestPutEndpoints(string url)
        {
            var response = await _client.PutAsync(url, null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/Seeders/Run/Players")]
        [InlineData("api/Seeders/Run/Rosters")]
        [InlineData("api/Seeders/Run/Teams")]
        [InlineData("api/Seeders/Run/TeamStats")]
        //[InlineData("api/Seeders/Refresh/Players")]
        //[InlineData("api/Seeders/Refresh/Teams")]
        public async Task TestPostEndpoints(string url)
        {
            var response = await _client.PostAsync(url, null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
