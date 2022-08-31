using EFCore.TrackingVsNoTracking.Console.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCore.TrackingVsNoTracking.Console.Context
{
    public class DemoContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data source=local;Initial Catalog=DemoDB;Integrated Security=True; Pooling=true; MultipleActiveResultSets=false";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }


}
