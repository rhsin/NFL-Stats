using NflStats.Models;
using System.Collections.Generic;
using System.Linq;

namespace NflStats.Services
{
    public interface IStatsCalculator
    {
        public List<Player> TDRatio(List<Player> players);
    }

    public class StatsCalculator : IStatsCalculator
    {
        // Calculates each player's TD/Turnover (Int + Fumbles) Ratio and returns 
        // Player list ordered by the ratio, set as the Points field.
        public List<Player> TDRatio(List<Player> players)
        {
            foreach (var p in players)
            {
                p.Points = (float)p.PassTds / (float)(p.PassInt + p.Fumbles);
            }   

            return players.OrderByDescending(p => p.Points).ToList();
        }
    }
}
