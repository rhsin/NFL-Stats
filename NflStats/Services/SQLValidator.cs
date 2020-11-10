using System;

namespace NflStats.Services
{
    public interface ISQLValidator
    {
        public string Column(string field, string type);
        public string Yards(string type);
        public string Touchdowns(string type);
        public string Turnovers(string type);
    }

    public class SQLValidator : ISQLValidator
    {
        // Determines which method used to return Players table column name.
        public string Column(string field, string type)
        {
            switch (field.ToLower())
            {
                case "yards":
                    return this.Yards(type);
                case "touchdowns":
                    return this.Touchdowns(type);
                case "turnovers":
                    return this.Turnovers(type);
                default:
                    throw new ArgumentException("Invalid Type Parameter!");
            }
        }

        public string Yards(string type)
        {
            switch (type.ToLower())
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

        public string Touchdowns(string type)
        {
            switch (type.ToLower())
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

        public string Turnovers(string type)
        {
            switch (type.ToLower())
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
