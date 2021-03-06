﻿using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NflStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NflStatsTests.Integration
{
    public class TeamStatTests : IClassFixture<WebApplicationFactory<NflStats.Startup>>
    {
        private readonly HttpClient _client;
        private readonly int _id = 1;

        public TeamStatTests(WebApplicationFactory<NflStats.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTeamStats()
        {
            var response = await _client.GetAsync("api/TeamStats");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teamStats = JsonConvert.DeserializeObject<List<TeamStat>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(96, teamStats.Count());
            Assert.All(teamStats, ts => Assert.NotEmpty(ts.TeamName));
            Assert.All(teamStats, ts => Assert.NotNull(ts.TeamId));
            Assert.Contains("Carolina Panthers", stringResponse);
            Assert.Contains("Oakland Raiders", stringResponse);
        }

        [Fact]
        public async Task GetTeamStat()
        {
            var response = await _client.GetAsync($"api/TeamStats/{_id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teamStat = JsonConvert.DeserializeObject<TeamStat>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Baltimore Ravens", teamStat.TeamName);
            Assert.Equal(29, teamStat.TeamId);
            Assert.Equal(6521, teamStat.TotalYds);
            Assert.Equal(3296, teamStat.RushYds);
        }

        [Fact]
        public async Task FindTeamStat()
        {
            var response = await _client.GetAsync($"api/TeamStats/Find?team=colts&season=2017");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teamStat = JsonConvert.DeserializeObject<TeamStat>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Indianapolis Colts", teamStat.TeamName);
            Assert.Equal(2017, teamStat.Season);
            Assert.Equal(18, teamStat.TeamId);
            Assert.Equal(4553, teamStat.TotalYds);
            Assert.Equal(7, teamStat.Team.Players.Count());
        }

        [Fact]
        public async Task GetTeamLeaders()
        {
            var response = await _client.GetAsync($"api/TeamStats/Leaders/Team?team=colt&season=2018");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teamStats = JsonConvert.DeserializeObject<Dictionary<string, Player>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(4, teamStats.Count());
            Assert.True(teamStats.ContainsKey("RB"));
            Assert.Equal("Andrew Luck", teamStats["QB"].Name);
            Assert.Equal("T.Y. Hilton", teamStats["WR"].Name);
            Assert.Equal(.768, Math.Round(teamStats["QB"].Points, 3));
        }

        [Fact]
        public async Task FindTeamLeaders()
        {
            var response = await _client.GetAsync($"api/TeamStats/Leaders/Find?team=chief&season=2018");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var teamStats = JsonConvert.DeserializeObject<List<object>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(7, teamStats.Count());
            Assert.Contains("Kansas City Chiefs", stringResponse);
            Assert.Contains("Patrick Mahomes", stringResponse);
            Assert.Contains("Tyreek Hill", stringResponse);
        }

        [Fact]
        public async Task SeedTeamStats()
        {
            var response = await _client.PostAsync("api/Seeders/Run/TeamStats", null);
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("TeamStats Already Seeded!", stringResponse);
        }

        //[Fact]
        //public async Task GetTeamLeaders()
        //{
        //    var response = await _client.GetAsync($"api/TeamStats/Leaders/Team?team=colt&season=2018");
        //    var stringResponse = await response.Content.ReadAsStringAsync();
        //    var teamStats = JsonConvert.DeserializeObject<List<object>>(stringResponse);

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    Assert.Equal(4, teamStats.Count());
        //    Assert.Contains("player", stringResponse);
        //    Assert.Contains("Andrew Luck", stringResponse);
        //    Assert.Contains("T.Y. Hilton", stringResponse);
        //}
    }
}
