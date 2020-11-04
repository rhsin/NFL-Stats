﻿using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NflStatsTests.Unit
{
    public class WebScraperTests
    {
        [Fact]
        public void GetPlayers()
        {
            var webScraper = new WebScraper();
            var players = webScraper.GetPlayers().Result;

            Assert.IsType<List<Player>>(players);
            Assert.True(players.Count() > 30);
            Assert.All(players, p => Assert.NotNull(p.Name));
        }
    }
}
