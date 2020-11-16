
namespace NflStats.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string TeamName { get; set; }
        public float Points { get; set; }
        public int? Season { get; set; }
        public float? Games { get; set; }
        public float? PassYds { get; set; }
        public float? PassTds { get; set; }
        public float? PassInt { get; set; }
        public float? RushYds { get; set; }
        public float? RushTds { get; set; }
        public float? RecYds { get; set; }
        public float? RecTds { get; set; }
        public float? Fumbles { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public int? RosterId { get; set; }
        public Roster Roster { get; set; }
    }
}
