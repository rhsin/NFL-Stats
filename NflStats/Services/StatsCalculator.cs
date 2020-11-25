using NflStats.Models;
using System.Collections.Generic;
using System.Linq;

namespace NflStats.Services
{
    public interface IStatsCalculator
    {
        public IList<Player> TotalTDs(IList<Player> players);
        public IList<Player> TDRatio(IList<Player> players);
        public IList<Player> ScrimmageYds(IList<Player> players);
    }

    public class StatsCalculator : IStatsCalculator
    {
        // Calculates each players total touchdowns and returns Player 
        // List ordered by the total, set as the Points field.
        public IList<Player> TotalTDs(IList<Player> players)
        {
            foreach (var p in players)
            {
                var total = (float)(p.PassTds + p.RushTds + p.RecTds);

                p.Points = total;
            }

            return players.OrderByDescending(p => p.Points).ToList();
        }

        // Calculates each players(QB) TD/Turnover Ratio and returns Player 
        // List ordered by the ratio, set as the Points field, if between 0, 100.
        public IList<Player> TDRatio(IList<Player> players)
        {
            foreach (var p in players.ToList())
            {
                var ratio = (float)((p.PassTds + p.RushTds + p.RecTds) / (p.PassInt + p.Fumbles));

                if (ratio > 0 && ratio < 100 && p.Position == "QB")
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

        // Calculates each players Yards from Scrimmage (Rush + Rec) and returns 
        // Player List ordered by the YFS, set as the Points field.
        public IList<Player> ScrimmageYds(IList<Player> players)
        {
            foreach (var p in players)
            {
                var yards = (float)(p.RushYds + p.RecYds);

                p.Points = yards;
            }
               
            return players.OrderByDescending(p => p.Points).ToList();
        }
    }
}
