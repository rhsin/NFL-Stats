using NflStats.Models;
using System.Linq;

namespace NflStats.Services
{
    public interface ILineupValidator
    {
        public bool Standard(Roster roster);
    }

    public class LineupValidator : ILineupValidator
    {
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
