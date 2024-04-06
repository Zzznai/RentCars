using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Data;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CarsController : Controller
    {
        private readonly RentCarDbContext dbContext;
        private readonly CloudinarySettings cloudinarySettings;
        private readonly Cloudinary cloudinary;

        public CarsController(RentCarDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            this.cloudinary = new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
        }

        public IActionResult Create()
        {
            var car = new CarCreateViewModel();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CarCreateViewModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            if (ModelState.IsValid)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(carModel.Image.FileName, carModel.Image.OpenReadStream()),
                    
                };

                var result = this.cloudinary.Upload(uploadParams);

                var imageUrl = result.Url;

                var car = new Car()
                {
                    Brand = carModel.Brand,
                    Model = carModel.Model,
                    EngineType = carModel.EngineType,
                    Year = carModel.Year,
                    PassengerCapacity = carModel.PassengerCapacity,
                    Description = carModel.Description,
                    RentalPricePerDay = carModel.RentalPricePerDay,
                    ImageUrl = imageUrl.OriginalString
                };

                await this.dbContext.Cars.AddAsync(car);
                await this.dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cars=this.dbContext.Cars.ToList();
            return View(cars);
        }

        [HttpPost]
        [Route("cars/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            this.dbContext.Remove(car);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
