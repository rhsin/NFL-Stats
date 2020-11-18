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
        [InlineData("api/Players/Season/2017")]
        [InlineData("api/Players/1845")]
        [InlineData("api/Players/Find?position=TE")]
        [InlineData("api/Players/Stats?field=yards&type=WR&value=900")]
        [InlineData("api/Players/Team?teamId=10&season=2017")]
        [InlineData("api/Stats/Fantasy/1827/9")]
        [InlineData("api/Rosters")]
        [InlineData("api/Rosters/1")]
        [InlineData("api/Teams")]
        [InlineData("api/Teams/15")]
        [InlineData("api/TeamStats")]
        [InlineData("api/TeamStats/75")]
        [InlineData("api/TeamStats/Find?team=falcons&season=2018")]
        [InlineData("api/TeamStats/Leaders/Team?team=falcons&season=2018")]
        [InlineData("api/TeamStats/Leaders/Find?team=arizona&season=2019")]
        [InlineData("api/Teams/Find?division=NFC West")]
        [InlineData("api/Stats/Fantasy/Rosters/1/9")]
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
