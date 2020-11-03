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
    }
}
