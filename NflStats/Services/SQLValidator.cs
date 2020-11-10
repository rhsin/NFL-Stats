using System;

namespace NflStats.Services
{
    public interface ISQLValidator
    {
        public string Yards(string input);
        public string Touchdowns(string input);
        public string Turnovers(string input);
    }

    public class SQLValidator : ISQLValidator
    {
        public string Yards(string input)
        {
            switch (input)
            {
                case "passing":
                    return "PassYds";
                case "rushing":
                    return "RushYds";
                case "receiving":
                    return "RecYds";
                case "QB":
                    return "PassYds";
                case "RB":
                    return "RushYds";
                case "WR":
                    return "RecYds";
                case "TE":
                    return "RecYds";
                default:
                    throw new ArgumentException("Passing, Rushing, Receiving Yards Only!");
            }
        }

        public string Touchdowns(string input)
        {
            switch (input)
            {
                case "passing":
                    return "PassTds";
                case "rushing":
                    return "RushTds";
                case "receiving":
                    return "RecTds";
                case "QB":
                    return "PassTds";
                case "RB":
                    return "RushTds";
                case "WR":
                    return "RecTds";
                case "TE":
                    return "RecTds";
                default:
                    throw new ArgumentException("Passing, Rushing, Receiving TDs Only!");
            }
        }

        public string Turnovers(string input)
        {
            switch (input)
            {
                case "interceptions":
                    return "PassInt";
                case "fumbles":
                    return "Fumbles";
                case "QB":
                    return "PassInt";
                case "RB":
                    return "Fumbles";
                case "WR":
                    return "Fumbles";
                case "TE":
                    return "Fumbles";
                default:
                    throw new ArgumentException("Interceptions, Fumbles Only!");
            }
        }
    }
}
