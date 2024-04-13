using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RentCars.Commons;
using RentCars.Data;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    /// <summary>
    /// Controller responsible for managing cars.
    /// </summary>
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CarsController : Controller
    {
        private readonly RentCarDbContext dbContext;
        private readonly CloudinarySettings cloudinarySettings;
        private readonly Cloudinary cloudinary;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarsController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for RentCars.</param>
        /// <param name="configuration">The configuration.</param>
        public CarsController(RentCarDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            this.cloudinary = new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
        }

        /// <summary>
        /// Displays the form for creating a new car.
        /// </summary>
        public IActionResult Create()
        {
            var car = new CarCreateViewModel();
            return View(car);
        }

        /// <summary>
        /// Handles the creation of a new car.
        /// </summary>
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

        /// <summary>
        /// Displays the list of cars.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            var cars = this.dbContext.Cars.ToList();
            return View(cars);
        }

        /// <summary>
        /// Displays the list of all cars (accessible to all users).
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AllCars()
        {
            var cars = this.dbContext.Cars.ToList();
            return View(cars);
        }

        /// <summary>
        /// Deletes a car.
        /// </summary>
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

        /// <summary>
        /// Displays the form for editing a car.
        /// </summary>
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

        /// <summary>
        /// Handles the editing of a car.
        /// </summary>
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

                // Retrieve the original rental price
                var originalRentalPricePerDay = car.RentalPricePerDay;

                // Update car properties
                car.Brand = carModel.Brand;
                car.PassengerCapacity = carModel.PassengerCapacity;
                car.Model = carModel.Model;
                car.EngineType = carModel.EngineType;
                car.RentalPricePerDay = carModel.RentalPricePerDay;
                car.Year = carModel.Year;
                car.Description = carModel.Description;

                // If there's a change in rental price, update reservation prices
                if (originalRentalPricePerDay != car.RentalPricePerDay)
                {
                    // Fetch reservations associated with this car
                    var reservations = await dbContext.Reservations.Where(r => r.Car.Id == car.Id).ToListAsync();

                    // Update total price for each reservation
                    foreach (var reservation in reservations)
                    {
                        // Calculate new total price based on the updated rental price
                        reservation.RentalSum = car.RentalPricePerDay * (reservation.EndDate.Subtract(reservation.StartDate).Days);

                        // Update reservation in database
                        dbContext.Entry(reservation).State = EntityState.Modified;
                    }
                }

                // Handle image upload if provided
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

                // Update car in database
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
