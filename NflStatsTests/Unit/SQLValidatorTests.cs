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

        [Theory]
        [InlineData("passing")]
        [InlineData("QB")]
        public void Yards(string input)
        {
            Assert.Equal("PassYds", _sqlValidator.Yards(input));
        }

        [Theory]
        [InlineData("passing")]
        [InlineData("QB")]
        public void Touchdowns(string input)
        {
            Assert.Equal("PassTds", _sqlValidator.Touchdowns(input));
        }

        [Theory]
        [InlineData("fumbles")]
        [InlineData("RB")]
        public void Turnovers(string input)
        {
            Assert.Equal("Fumbles", _sqlValidator.Turnovers(input));
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
