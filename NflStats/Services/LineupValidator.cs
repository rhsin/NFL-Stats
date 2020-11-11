using NflStats.Models;
using System;
using System.Linq;

namespace NflStats.Services
{
    public interface ILineupValidator
    {
        public float TotalPoints(Roster roster);
        public bool Standard(Roster roster);
    }

    public class LineupValidator : ILineupValidator
    {
        // Calculates total fantasy points if lineup is standard & valid.
        public float TotalPoints(Roster roster)
        {
            if (this.Standard(roster))
            {
                return roster.Players.Sum(p => p.Points);
            }
            else
            {
                throw new ArgumentException("Fantasy Lineup Not Valid!");
            }
        }

        // Checks if Players list in Roster meets standard fantasy lineup requirements.
        public bool Standard(Roster roster)
        {
            var players = roster.Players;

            if (players.Count() > 7)
            {
                return false;
            }
            else if (players.Where(p => p.Position == "QB").Count() > 1)
            {
                return false;
            }
            else if (players.Where(p => p.Position == "RB").Count() > 3)
            {
                return false;
            }
            else if (players.Where(p => p.Position == "WR").Count() > 3)
            {
                return false;
            }
            else if (players.Where(p => p.Position == "TE").Count() > 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
