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
        public IActionResult Create(CarCreateViewModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            if (ModelState.IsValid)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(carModel.Image.FileName, carModel.Image.OpenReadStream())
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

                this.dbContext.Cars.Add(car);
                this.dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
