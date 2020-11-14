using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using Xunit;

namespace NflStatsTests.Unit
{
    public class StatsCalculatorTests
    {
        private readonly StatsCalculator _statsCalculator;

        public StatsCalculatorTests()
        {
            _statsCalculator = new StatsCalculator();
        }

        [Fact]
        public void TDRatio()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", PassTds = 9, RushTds = 1, RecTds = 0, PassInt = 2, Fumbles = 3, Points = 0 },
                new Player { Position = "QB", PassTds = 13, RushTds = 1, RecTds = 1, PassInt = 3, Fumbles = 2, Points = 0 },
                new Player { Position = "QB", PassTds = 1, RushTds = 0, RecTds = 0, PassInt = 0, Fumbles = 0, Points = 0 },
                new Player { Position = "WR", PassTds = 0, RushTds = 1, RecTds = 14, PassInt = 0, Fumbles = 6, Points = 0 }
            };
            var result = _statsCalculator.TDRatio(players);

            Assert.Collection(result, 
                item => 
                {
                    Assert.Equal(3, item.Points);
                    Assert.Equal(13, item.PassTds);
                },
                item =>
                {
                    Assert.Equal(2, item.Points);
                    Assert.Equal(9, item.PassTds);
                });
        }

        [Fact]
        public void ScrimmageYds()
        {
            var players = new List<Player>
            {
                new Player { RushYds = 1000, RecYds = 200, Points = 0 },
                new Player { RushYds = 500, RecYds = 1200, Points = 0 }
            };

            var result = _statsCalculator.ScrimmageYds(players);

            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1700, item.Points);
                    Assert.Equal(500, item.RushYds);
                },
                item =>
                {
                    Assert.Equal(1200, item.Points);
                    Assert.Equal(1000, item.RushYds);
                });
        }
    }
}