using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AllCars()
        {
            var cars = this.dbContext.Cars.ToList();
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

        [HttpGet]
        [Route("cars/carsdatesinfo/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> CarsDatesInfo(int id)
        {
            var car = await this.dbContext.Cars.Include(c => c.Reservations).FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var carModel = new CarAvailableDates
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Reservations = car.Reservations != null ? car.Reservations.ToList() : new List<Reservation>()
            };

            return View("CarsDatesInfo", carModel);
        }




        [HttpGet]
        [Route("cars/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await this.dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var carModel = new CarEditViewModel()
            {
                Brand = car.Brand,
                Model = car.Model,
                EngineType = car.EngineType,
                Year = car.Year,
                PassengerCapacity = car.PassengerCapacity,
                Description = car.Description,
                RentalPricePerDay = car.RentalPricePerDay,
            };

            return View(carModel);
        }

        [HttpPost]
        [Route("cars/edit/{id}")]
        public async Task<IActionResult> Edit(int id, CarEditViewModel carModel)
        {
            if (ModelState.IsValid)
            {
                var car = await this.dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);

                if (car == null)
                {
                    return NotFound();
                }

                car.Brand = carModel.Brand;
                car.PassengerCapacity = carModel.PassengerCapacity;
                car.Model = carModel.Model;
                car.EngineType = carModel.EngineType;
                car.RentalPricePerDay = carModel.RentalPricePerDay;
                car.Year = carModel.Year;
                car.Description = carModel.Description;

                if (carModel.Image != null)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(carModel.Image.FileName, carModel.Image.OpenReadStream()),
                    };

                    var result = this.cloudinary.Upload(uploadParams);

                    var imageUrl = result.Url;

                    car.ImageUrl = imageUrl.OriginalString;
                  
                }

                this.dbContext.Cars.Update(car);
                await this.dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return View(carModel);
            }
        }

    }
}
