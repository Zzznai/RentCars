using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Commons.Enums;
using RentCars.Data;
using RentCars.Models;
using RentCars.ViewModels;
using System.Diagnostics;

namespace RentCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentCarDbContext rentCarDbContext;

        public HomeController(ILogger<HomeController> logger, RentCarDbContext rentCarDbContext)
        {
            _logger = logger;
            this.rentCarDbContext = rentCarDbContext;
        }

        public IActionResult Index()
        {
            var searchModel = new SearchViewModel();

            if (User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return RedirectToAction("AdminIndex");
            }

            var cars = new List<Car>();

            var sessionStartDate = HttpContext.Session.GetString("searchStartDate");
            var sessionEndDate = HttpContext.Session.GetString("searchEndDate");

            if (sessionStartDate != null && sessionEndDate!=null)
            {
                DateTime startDate = DateTime.Parse(sessionStartDate);
                DateTime endDate = DateTime.Parse(sessionEndDate);

                cars = rentCarDbContext.Cars
           .Where(car => !car.Reservations.Any(reservation =>
               (reservation.Status == ReservationStatus.Denied || reservation.Status == ReservationStatus.Waiting) &&
               (reservation.StartDate <= endDate && reservation.EndDate >= startDate)))
           .ToList();
                searchModel.StartDate = startDate;
                searchModel.EndDate = endDate;
            }

            searchModel.Cars = cars;

            return View(searchModel);
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel searchModel)
        {
            if (User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return RedirectToAction("AdminIndex");
            }

            var cars = this.rentCarDbContext.Cars
            .Where(r => !r.Reservations.Any(reservation =>
                (reservation.StartDate <= searchModel.EndDate && reservation.EndDate >= searchModel.StartDate)))
            .ToList();

            HttpContext.Session.SetString("searchStartDate", searchModel.StartDate.ToString());
            HttpContext.Session.SetString("searchEndDate", searchModel.EndDate.ToString());

            searchModel.Cars = cars;

            return View(searchModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
