using System.Collections.Generic;

namespace NflStats.Models
{
    public class Roster
    {
        public int Id { get; set; }
        public string Team { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
