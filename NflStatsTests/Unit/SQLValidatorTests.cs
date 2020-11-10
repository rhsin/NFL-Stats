using NflStats.Services;
using System;
using Xunit;

namespace NflStatsTests.Unit
{
    public class SQLValidatorTests
    {
        private readonly SQLValidator _sqlValidator;

        public SQLValidatorTests()
        {
            _sqlValidator = new SQLValidator();
        }

        [Fact]
        public void Column()
        {
            Assert.Equal("RushYds", _sqlValidator.Column("Yards", "rushing"));
            Assert.Equal("RecTds", _sqlValidator.Column("Touchdowns", "WR"));
            Assert.Equal("Fumbles", _sqlValidator.Column("turnovers", "RB"));
        }

        [Theory]
        [InlineData("passing")]
        [InlineData("QB")]
        public void Yards(string type)
        {
            Assert.Equal("PassYds", _sqlValidator.Yards(type));
        }

        [Theory]
        [InlineData("passing")]
        [InlineData("QB")]
        public void Touchdowns(string type)
        {
            Assert.Equal("PassTds", _sqlValidator.Touchdowns(type));
        }

        [Theory]
        [InlineData("fumbles")]
        [InlineData("RB")]
        public void Turnovers(string type)
        {
            Assert.Equal("Fumbles", _sqlValidator.Turnovers(type));
        }

        [Fact]
        public void ColumnException()
        {
            try
            {
                _sqlValidator.Column("tackles", "CB");
            }
            catch (Exception ex)
            {
                Assert.Equal("Invalid Type Parameter!", ex.Message);
            }
        }

        [Fact]
        public void YardsException()
        {
            try
            {
                _sqlValidator.Yards("CB");
            }
            catch (Exception ex)
            {
                Assert.Equal("Passing, Rushing, Receiving Yards Only!", ex.Message);
            }
        }

        [Fact]
        public void TouchdownsException()
        {
            try
            {
                _sqlValidator.Touchdowns("CB");
            }
            catch (Exception ex)
            {
                Assert.Equal("Passing, Rushing, Receiving TDs Only!", ex.Message);
            }
        }

        [Fact]
        public void TurnoversException()
        {
            try
            {
                _sqlValidator.Turnovers("CB");
            }
            catch (Exception ex)
            {
                Assert.Equal("Interceptions, Fumbles Only!", ex.Message);
            }
        }
    }
}
