using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons;
using RentCars.Data;

namespace RentCars.Controllers
{
    /// <summary>
    /// Controller responsible for admin-specific actions.
    /// </summary>
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly RentCarDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AdminController(RentCarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Displays all reservations sorted by start date, end date, and car brand.
        /// </summary>
        /// <returns>The view containing all reservations.</returns>
        public IActionResult AllReservations()
        {
            var reservations = this.dbContext.Reservations
                .Include(c => c.Car)
                .Include(u => u.User)
                .OrderBy(r => r.StartDate)
                .ThenBy(r => r.EndDate)
                .ThenBy(r => r.Car.Brand)
                .ToList();

            return View(reservations);
        }
    }
}
