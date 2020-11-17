using Microsoft.EntityFrameworkCore;
using NflStats.Models;

namespace NflStats.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base (options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamStat> TeamStats { get; set; }
        public DbSet<Roster> Rosters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne<Team>(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Player>()
                .HasOne<Roster>(p => p.Roster)
                .WithMany(r => r.Players)
                .HasForeignKey(p => p.RosterId);

            modelBuilder.Entity<TeamStat>()
                .HasOne<Team>(ts => ts.Team)
                .WithMany(t => t.TeamStats)
                .HasForeignKey(ts => ts.TeamId);
        }
    }
}
