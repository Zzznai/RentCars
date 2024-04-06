using Microsoft.AspNetCore.Mvc;

namespace RentCars.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
