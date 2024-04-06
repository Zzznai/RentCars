using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Models;
using System.Diagnostics;

namespace RentCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
