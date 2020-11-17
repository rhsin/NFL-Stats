
namespace NflStats.Models
{
    public class TeamStat
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int? Season { get; set; }
        public float Points { get; set; }
        public float TotalYds { get; set; }
        public float Turnovers { get; set; }
        public float PassYds { get; set; }
        public float PassTds { get; set; }
        public float RushYds { get; set; }
        public float RushTds { get; set; }
        public float YdsPerAtt { get; set; }
        public float PenYds { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
