using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons;
using RentCars.Models;
using System;
using System.Threading.Tasks;

namespace RentCars.Data
{
    /// <summary>
    /// Provides methods for seeding the database with admin user and roles.
    /// </summary>
    public class AdminUserSeeder
    {
        /// <summary>
        /// Initializes the roles required for the application.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new RentCarDbContext(serviceProvider.GetRequiredService<DbContextOptions<RentCarDbContext>>()))
            {
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
                }

                if (!await roleManager.RoleExistsAsync(GlobalConstants.UserRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.UserRoleName));
                }
            }
        }

        /// <summary>
        /// Adds an admin user to the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static async Task AddAdminUser(IServiceProvider serviceProvider)
        {
            using (var dbContext = new RentCarDbContext(serviceProvider.GetRequiredService<DbContextOptions<RentCarDbContext>>()))
            {
                var userManager = serviceProvider.GetService<UserManager<RentCarUser>>();

                var user = await userManager.FindByEmailAsync("admin@rentcars.bg");

                if (user == null)
                {
                    user = new RentCarUser()
                    {
                        FirstName = "Naim",
                        LastName = "Abaz",
                        Email = "admin@rentcars.bg",
                        PhoneNumber = "0884102500",
                        UniqueCitinzenshipNumber = "1234567890",
                        UserName = "Admin"
                    };

                    await userManager.CreateAsync(user);

                    await userManager.AddPasswordAsync(user, "Admin2005!");

                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
