using Microsoft.EntityFrameworkCore;
using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DepartureAirport)
                .WithMany()
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.ArrivalAirport)
                .WithMany()
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}