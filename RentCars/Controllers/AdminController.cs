using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons;
using RentCars.Data;

namespace RentCars.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]

    public class AdminController : Controller
    {
        private readonly RentCarDbContext dbContext;

        public AdminController(RentCarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult AllReservations()
        {
            var reservations = this.dbContext.Reservations
                .Include(c => c.Car)
                .Include(u => u.User)
                .OrderBy(r => r.StartDate)
                .ThenBy(r => r.EndDate)
                .ThenBy(r=>r.Car.Brand)
                .ToList();

            return View(reservations);
        }
    }
}
