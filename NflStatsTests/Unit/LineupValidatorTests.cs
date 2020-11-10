using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using Xunit;

namespace NflStatsTests.Unit
{
    public class LineupValidatorTests
    {
        [Fact]
        public void Standard()
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

            Assert.True(lineupValidator.Standard(roster));
        }

        [Fact]
        public void StandardFalse()
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

            Assert.False(lineupValidator.Standard(roster));
        }
    }
}
