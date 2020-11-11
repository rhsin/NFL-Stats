using NflStats.Models;
using NflStats.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace NflStatsTests.Unit
{
    public class LineupValidatorTests
    {
        private readonly LineupValidator _lineupValidator;

        public LineupValidatorTests()
        {
            _lineupValidator = new LineupValidator();
        }

        [Fact]
        public void TotalPoints()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", Points = 8 },
                new Player { Position = "RB", Points = 2 }
            };

            Assert.Equal(10, _lineupValidator.TotalPoints(players));
        }

        [Fact]
        public void TotalPointsException()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB", Points = 8 },
                new Player { Position = "QB", Points = 2 }
            };

            try
            {
                _lineupValidator.TotalPoints(players);
            }
            catch (Exception ex)
            {
                Assert.Equal("Fantasy Lineup Not Valid!", ex.Message);
            }
        }

        [Fact]
        public void Standard()
        {
            var players = new List<Player>
            {
                new Player { Position = "QB" },
                new Player { Position = "WR" }
            };

            Assert.True(_lineupValidator.Standard(players));
        }

        [Fact]
        public void StandardFalse()
        {
            var players = new List<Player>
            {
                new Player { Position = "TE" },
                new Player { Position = "TE" },
                new Player { Position = "TE" }
            };

            Assert.False(_lineupValidator.Standard(players));
        }
    }
}
