using NflStats.Models;
using NflStats.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NflStatsTests.Unit
{
    public class CsvImporterTests
    {
        [Fact]
        public void GetPlayerRecords2019()
        {
            var csvImporter = new CsvImporter();

            var players = csvImporter.GetPlayerRecords(2019);

            Assert.IsType<List<Player>>(players);
            Assert.Equal(620, players.Count());
            Assert.All(players, p => Assert.NotNull(p.Name));
        }

        [Fact]
        public void GetPlayerRecords2018()
        {
            var csvImporter = new CsvImporter();

            var players = csvImporter.GetPlayerRecords(2018);

            Assert.IsType<List<Player>>(players);
            Assert.Equal(622, players.Count());
            Assert.All(players, p => Assert.NotNull(p.Name));
        }
    }
}
