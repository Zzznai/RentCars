using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons.Enums;
using RentCars.Models;

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

            base.OnModelCreating(builder);
        }
    }
}
