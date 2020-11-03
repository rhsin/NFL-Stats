
namespace NflStats.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public float Points { get; set; }

        public int? RosterId { get; set; }
        public Roster Roster { get; set; }
    }
}
