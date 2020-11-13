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
        public IEnumerable<Player> GetPlayerRecords(int year);
    }

    public class CsvImporter : ICsvImporter
    {
        // Reads Csv file by year and converts each row into Player entity using Player ClassMap.
        public IEnumerable<Player> GetPlayerRecords(int year)
        {
            using (var reader = new StreamReader(@$"C:\Users\Ryan\source\repos\NflStats\NflStats\Data\{year}.csv"))
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
            Map(m => m.Games).Name("G");
            Map(m => m.PassYds).Name("PassingYds");
            Map(m => m.PassTds).Name("PassingTD");
            Map(m => m.PassInt).Name("Int");
            Map(m => m.RushYds).Name("RushingYds");
            Map(m => m.RushTds).Name("RushingTD");
            Map(m => m.RecYds).Name("ReceivingYds");
            Map(m => m.RecTds).Name("ReceivingTD");
            Map(m => m.Fumbles).Name("Fumbles");
        }
    }
}
