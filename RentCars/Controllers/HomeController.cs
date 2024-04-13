using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
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

            var sessionStartDate = HttpContext.Session.GetString("searchStartDate");
            var sessionEndDate = HttpContext.Session.GetString("searchEndDate");

            if (sessionStartDate != null && sessionEndDate != null)
            {
                searchModel.StartDate = DateTime.Parse(sessionStartDate);
                searchModel.EndDate = DateTime.Parse(sessionEndDate);
            }
            else
            {
                searchModel.StartDate = DateTime.Now.Date;
                searchModel.EndDate = searchModel.StartDate.AddDays(1);
            }


            searchModel.Cars = GetAllAvailableCars(searchModel.StartDate, searchModel.EndDate);

            return View(searchModel);
        }


        [HttpPost]
        public IActionResult Index(SearchViewModel searchModel)
        {
            if (User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return RedirectToAction("AdminIndex");
            }

            HttpContext.Session.SetString("searchStartDate", searchModel.StartDate.ToString());
            HttpContext.Session.SetString("searchEndDate", searchModel.EndDate.ToString());

            searchModel.Cars = GetAllAvailableCars(searchModel.StartDate, searchModel.EndDate);

            return View(searchModel);
        }

        public List<Car> GetAllAvailableCars(DateTime searchStartDate,  DateTime searchEndDate)
        {
            var carsWithNoReservations = rentCarDbContext.Cars
         .Where(car => !car.Reservations.Any(r =>
             (r.StartDate <= searchEndDate && r.EndDate >= searchStartDate) && // Check for overlapping reservations
             r.Status != ReservationStatus.Denied)) // Exclude denied reservations
         .ToList();

            return carsWithNoReservations;
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
