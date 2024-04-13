using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons.Enums;
using RentCars.Models;
using System.Reflection.Emit;
using System.Security.Claims;

namespace RentCars.Data
{
    /// <summary>
    /// Represents the database context for the RentCars application.
    /// </summary>
    public class RentCarDbContext : IdentityDbContext<RentCarUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentCarDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public RentCarDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentCarDbContext"/> class.
        /// </summary>
        public RentCarDbContext()
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for cars.
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for reservations.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

        /// <summary>
        /// Configures the model for the database.
        /// </summary>
        /// <param name="builder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Convert enums to strings for storage in the database
            builder
                .Entity<Car>()
                .Property(c => c.EngineType)
                .HasConversion(e => e.ToString(), e => (EngineType)Enum.Parse(typeof(EngineType), e));

            builder
                .Entity<Reservation>()
                .Property(r => r.Status)
                .HasConversion(e => e.ToString(), e => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), e));

            // Ensure unique constraints for email, username, and citizenship number
            builder.Entity<RentCarUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<RentCarUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<RentCarUser>()
                .HasIndex(u => u.UniqueCitinzenshipNumber)
                .IsUnique();

            // Configure precision and scale for rental price per day and rental sum properties
            builder.Entity<Car>().Property(car => car.RentalPricePerDay).HasPrecision(8, 2);

            builder.Entity<Reservation>().Property(r => r.RentalSum).HasPrecision(8, 2);

            base.OnModelCreating(builder);
        }
    }
}
