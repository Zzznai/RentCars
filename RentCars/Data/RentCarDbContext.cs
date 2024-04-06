using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons.Enums;
using RentCars.Models;
using System.Reflection.Emit;
using System.Security.Claims;

namespace RentCars.Data
{
    public class RentCarDbContext : IdentityDbContext<RentCarUser>
    {
        public RentCarDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .Property(c => c.EngineType)
                .HasConversion(e => e.ToString(), e => (EngineType)Enum.Parse(typeof(EngineType), e));

            builder
                .Entity<Reservation>()
                .Property(r => r.Status)
                .HasConversion(e => e.ToString(), e => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), e));

            builder.Entity<RentCarUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<RentCarUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<RentCarUser>()
                .HasIndex(u => u.UniqueCitinzenshipNumber)
            .IsUnique();

            builder.Entity<Car>().Property(car => car.RentalPricePerDay).HasPrecision(8, 2);

            builder.Entity<Reservation>().Property(r => r.RentalSum).HasPrecision(8, 2);

            base.OnModelCreating(builder);
        }
    }
}
