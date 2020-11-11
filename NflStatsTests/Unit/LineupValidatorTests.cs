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
            var roster = new Roster
            {
                Players = new List<Player>
                {
                    new Player { Position = "QB", Points = 8 },
                    new Player { Position = "RB", Points = 2 }
                }
            };

            Assert.Equal(10, _lineupValidator.TotalPoints(roster));
        }

        [Fact]
        public void TotalPointsException()
        {
            var roster = new Roster
            {
                Players = new List<Player>
                {
                    new Player { Position = "QB", Points = 8 },
                    new Player { Position = "QB", Points = 2 }
                }
            };

            try
            {
                _lineupValidator.TotalPoints(roster);
            }
            catch (Exception ex)
            {
                Assert.Equal("Fantasy Lineup Not Valid!", ex.Message);
            }
        }

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

            Assert.True(_lineupValidator.Standard(roster));
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

            Assert.False(_lineupValidator.Standard(roster));
        }
    }
}
