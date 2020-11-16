using System.Collections.Generic;

namespace NflStats.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
