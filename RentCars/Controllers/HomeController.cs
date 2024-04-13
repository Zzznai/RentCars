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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RentCars.Controllers
{
    /// <summary>
    /// Controller responsible for handling home-related actions.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentCarDbContext rentCarDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The ILogger for logging.</param>
        /// <param name="rentCarDbContext">The database context for RentCars.</param>
        public HomeController(ILogger<HomeController> logger, RentCarDbContext rentCarDbContext)
        {
            _logger = logger;
            this.rentCarDbContext = rentCarDbContext;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
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

        /// <summary>
        /// Handles the search form submission.
        /// </summary>
        /// <param name="searchModel">The search parameters.</param>
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

        /// <summary>
        /// Retrieves all available cars for a given date range.
        /// </summary>
        /// <param name="searchStartDate">The start date of the search.</param>
        /// <param name="searchEndDate">The end date of the search.</param>
        /// <returns>A list of available cars.</returns>
        public List<Car> GetAllAvailableCars(DateTime searchStartDate, DateTime searchEndDate)
        {
            var carsWithNoReservations = rentCarDbContext.Cars
                .Where(car => !car.Reservations.Any(r =>
                    (r.StartDate <= searchEndDate && r.EndDate >= searchStartDate) && // Check for overlapping reservations
                    r.Status != ReservationStatus.Denied)) // Exclude denied reservations
                .ToList();

            return carsWithNoReservations;
        }

        /// <summary>
        /// Displays the admin home page.
        /// </summary>
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AdminIndex()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy page.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the about page.
        /// </summary>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Handles errors and displays the error page.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
