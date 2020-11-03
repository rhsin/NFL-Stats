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
        public DbSet<Roster> Rosters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne<Roster>(p => p.Roster)
                .WithMany(r => r.Players)
                .HasForeignKey(p => p.RosterId);
        }
    }
}
