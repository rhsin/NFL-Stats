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
        // Player list ordered by the ratio, set as the Points field, if between 0, 100.
        public List<Player> TDRatio(List<Player> players)
        {
            foreach (var p in players.ToList())
            {
                float ratio = (float)p.PassTds / (float)(p.PassInt + p.Fumbles);

                if (ratio > 0 && ratio < 100)
                {
                    p.Points = ratio;
                }
                else
                {
                    players.Remove(p);
                }
            }   

            return players.OrderByDescending(p => p.Points).ToList();
        }
    }
}
