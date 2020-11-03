using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using Xunit;

namespace NflStatsTests.Unit
{
    public class LineupValidatorTests
    {
        [Fact]
        public void IsStandard()
        {
            var roster = new Roster
            {
                Players = new List<Player>
                {
                    new Player { Position = "QB" },
                    new Player { Position = "RB" }
                }
            };
            var lineupValidator = new LineupValidator();

            Assert.True(lineupValidator.IsStandard(roster));
        }

        [Fact]
        public void IsStandardFalse()
        {
            var roster = new Roster
            {
                Players = new List<Player>
                {
                    new Player { Position = "QB" },
                    new Player { Position = "QB" }
                }
            };
            var lineupValidator = new LineupValidator();

            Assert.False(lineupValidator.IsStandard(roster));
        }
    }
}
