using NflStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NflStats.Services
{
    public interface ILineupValidator
    {
        public float TotalPoints(List<Player> players);
        public bool Standard(List<Player> players);
    }

    public class LineupValidator : ILineupValidator
    {
        // Calculates total fantasy points if Player lineup is standard & valid.
        public float TotalPoints(List<Player> players)
        {
            if (this.Standard(players))
            {
                return players.Sum(p => p.Points);
            }
            else
            {
                throw new ArgumentException("Fantasy Lineup Not Valid!");
            }
        }

        // Checks if Players list meets standard fantasy lineup requirements.
        public bool Standard(List<Player> players)
        {
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
