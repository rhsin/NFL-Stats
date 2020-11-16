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
        public IEnumerable<Team> GetTeamRecords();
    }

    public class CsvImporter : ICsvImporter
    {
        // Reads Csv file by year and converts each row into Player entity using Player ClassMap.
        public IEnumerable<Player> GetPlayerRecords(int year)
        {
            using (var reader = new StreamReader(@$"C:\Users\Ryan\source\repos\NflStats\NflStats\Data\CSV\{year}.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<PlayerMap>();
                var records = csv.GetRecords<Player>();

                return records.ToList();
            }
        }

        // Reads Csv file by year and converts each row into Team entity using Team ClassMap.
        public IEnumerable<Team> GetTeamRecords()
        {
            using (var reader = new StreamReader(@"C:\Users\Ryan\source\repos\NflStats\NflStats\Data\CSV\nfl_teams.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<TeamMap>();
                var records = csv.GetRecords<Team>();

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
            Map(m => m.TeamName).Name("Tm");
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

    public class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Map(m => m.Name).Name("team_name");
            Map(m => m.Alias).Name("team_id");
            Map(m => m.Conference).Name("team_conference");
            Map(m => m.Division).Name("team_division");
        }
    }
}
