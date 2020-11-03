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
        public void GetPlayerRecords()
        {
            var csvImporter = new CsvImporter();

            var players = csvImporter.GetPlayerRecords();

            Assert.IsType<List<Player>>(players);
            Assert.Equal(620, players.Count());
        }
    }
}
