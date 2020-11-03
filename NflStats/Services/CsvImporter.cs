using CsvHelper;
using CsvHelper.Configuration;
using NflStats.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace NflStats.Services
{
    public interface ICsvImporter
    {
        public List<Player> GetPlayerRecords();
    }

    public class CsvImporter : ICsvImporter
    {
        public List<Player> GetPlayerRecords()
        {
            using (var reader = new StreamReader(@"C:\Users\Ryan\source\repos\NflStats\NflStats\Data\2019.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<PlayerMap>();
                var records = csv.GetRecords<Player>();

                return records.ToList();
            }
        }
    }

    public class PlayerMap : ClassMap<Player>
    {
        public PlayerMap()
        {
            Map(m => m.Name).Name("Player");
            Map(m => m.Position).Name("Pos");
            Map(m => m.Team).Name("Tm");
            Map(m => m.Points).Name("FantasyPoints");
        }
    }
}
