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
                new Player { PassTds = 10, PassInt = 2, Fumbles = 3, Points = 0 },
                new Player { PassTds = 15, PassInt = 3, Fumbles = 2, Points = 0 },
            };

            var result = _statsCalculator.TDRatio(players);

            Assert.Collection(result, 
                item => 
                {
                    Assert.Equal(3, item.Points);
                    Assert.Equal(15, item.PassTds);
                },
                item =>
                {
                    Assert.Equal(2, item.Points);
                    Assert.Equal(10, item.PassTds);
                });
        }
    }
}