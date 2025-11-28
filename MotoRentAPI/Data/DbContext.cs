using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Models;

namespace MotoRentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>().HasIndex(m => m.Plate).IsUnique();
            modelBuilder.Entity<DeliveryDriver>().HasIndex(d => d.CNPJ).IsUnique();
            modelBuilder.Entity<DeliveryDriver>().HasIndex(d => d.DriverLicenseNumber).IsUnique();

            modelBuilder
                .Entity<Rental>()
                .HasOne(r => r.Motorcycle)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MotorcycleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Rental>()
                .HasOne(r => r.DeliveryDriver)
                .WithMany(d => d.Rentals)
                .HasForeignKey(r => r.DeliveryDriverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
