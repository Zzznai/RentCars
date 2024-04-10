using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons;
using RentCars.Commons.Enums;
using RentCars.Data;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {

        private readonly RentCarDbContext dbContext;

        public ReservationsController(RentCarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("reservations/reserve/{carId}")]
        public async Task<IActionResult> Reserve(int carId, ReservationCreateViewModel reservationModel)
        {
            var car = await this.dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));

            if (car == null)
            {
                return NotFound();
            }

            Reservation reservation = new Reservation();
            reservation.StartDate = reservationModel.StartDate;
            reservation.EndDate = reservationModel.EndDate;
            reservation.Status = ReservationStatus.Waiting;
            reservation.RentalSum = car.RentalPricePerDay * (reservationModel.EndDate.Subtract(reservationModel.StartDate).Days);
            reservation.User = user;
            reservation.Car = car;

            await this.dbContext.Reservations.AddAsync(reservation);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reservations = this.dbContext.Reservations
            .Where(r => r.User.UserName.Equals(User.Identity.Name))
                .Include(r => r.Car)
                .Include(r => r.User)
                .OrderBy(r => r.StartDate)
                .ThenBy(r => r.Car.Brand)
                .ToList();
            return this.View(reservations);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> CarReservations(int id)
        {
            var reservations = this.dbContext.Reservations
            .Where(c => c.Car.Id == id)
                .Include(r => r.Car)
                .Include(r => r.User)
                .OrderBy(r => r.StartDate)
                .ThenBy(r => r.User.UserName)
                .ToList();
            return this.View(reservations);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> UserReservatios(string id)
        {
            var reservations = this.dbContext.Reservations
             .Where(r => r.User.Id == id)
                 .Include(r => r.Car)
                 .Include(r => r.User)
                 .OrderBy(r => r.StartDate)
                 .ThenBy(r => r.Car.Brand)
                 .ThenBy(r => r.Car.Model)
                 .ToList();
            return this.View(reservations);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        [Route("reservations/updateStatus/{reservationsId}")]
        public async Task<IActionResult> UpdateStatus(int reservationsId, string status)
        {
            var reservation = await this.dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationsId);

            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Status = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), status);

            this.dbContext.Update(reservation);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("AllReservations", "Admin");
        }
    }
}
