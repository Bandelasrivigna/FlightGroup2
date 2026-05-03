using Group2Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Group2Flight.Models.DataLayer
{
    public class Group2FlightDatabaseContext : DbContext
    {
        public Group2FlightDatabaseContext(DbContextOptions<Group2FlightDatabaseContext> options)
            : base(options) { }
        public DbSet<Airline> Airline { get; set; } = null!;
        public DbSet<Flight> Flight { get; set; } = null!;
        public DbSet<FlightSelection> FlightSelection { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureFlight());
            modelBuilder.ApplyConfiguration(new ConfigureAirline());
        }
    }
}
