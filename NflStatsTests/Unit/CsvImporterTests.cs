using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NflStatsTests.Unit
{
    public class CsvImporterTests
    {
        private readonly CsvImporter _csvImporter;

        public CsvImporterTests()
        {
            _csvImporter = new CsvImporter();
        }

        [Fact]
        public void GetPlayerRecords2019()
        {
            var players = _csvImporter.GetPlayerRecords(2019);

            Assert.IsType<List<Player>>(players);
            Assert.Equal(620, players.Count());
            Assert.All(players, p => Assert.NotNull(p.Name));
        }

        [Fact]
        public void GetPlayerRecords2018()
        {
            var players = _csvImporter.GetPlayerRecords(2018);

            Assert.IsType<List<Player>>(players);
            Assert.Equal(622, players.Count());
            Assert.All(players, p => Assert.NotNull(p.Name));
        }

        [Fact]
        public void GetTeamRecords()
        {
            var teams = _csvImporter.GetTeamRecords();

            Assert.IsType<List<Team>>(teams);
            Assert.Equal(32, teams.Count());
            Assert.All(teams, t => Assert.NotNull(t.Name));
        }

        [Fact]
        public void GetTeamStatRecords2019()
        {
            var teamStats = _csvImporter.GetTeamStatRecords(2019);

            Assert.IsType<List<TeamStat>>(teamStats);
            Assert.Equal(32, teamStats.Count());
            Assert.All(teamStats, ts => Assert.NotNull(ts.TeamName));
        }
    }
}
