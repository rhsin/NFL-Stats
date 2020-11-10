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
            switch (input.ToLower())
            {
                case "passing":
                    return "PassYds";
                case "rushing":
                    return "RushYds";
                case "receiving":
                    return "RecYds";
                case "qb":
                    return "PassYds";
                case "rb":
                    return "RushYds";
                case "wr":
                    return "RecYds";
                case "te":
                    return "RecYds";
                default:
                    throw new ArgumentException("Passing, Rushing, Receiving Yards Only!");
            }
        }

        public string Touchdowns(string input)
        {
            switch (input.ToLower())
            {
                case "passing":
                    return "PassTds";
                case "rushing":
                    return "RushTds";
                case "receiving":
                    return "RecTds";
                case "qb":
                    return "PassTds";
                case "rb":
                    return "RushTds";
                case "wr":
                    return "RecTds";
                case "te":
                    return "RecTds";
                default:
                    throw new ArgumentException("Passing, Rushing, Receiving TDs Only!");
            }
        }

        public string Turnovers(string input)
        {
            switch (input.ToLower())
            {
                case "interceptions":
                    return "PassInt";
                case "fumbles":
                    return "Fumbles";
                case "qb":
                    return "PassInt";
                case "rb":
                    return "Fumbles";
                case "wr":
                    return "Fumbles";
                case "te":
                    return "Fumbles";
                default:
                    throw new ArgumentException("Interceptions, Fumbles Only!");
            }
        }
    }
}
