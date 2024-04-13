using Microsoft.AspNetCore.Http;
using RentCars.Commons.Enums;
using RentCars.Models;
using RentCars.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Tests
{
    /// <summary>
    /// Unit tests for the CarEditViewModel
    /// </summary>
    [TestClass]
    public class CarEditViewModelTests
    {
        private IFormFile CreateValidFormFile(string content, string fileName)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return new FormFile(stream, 0, stream.Length, "Image", fileName);
        }

        [TestMethod]
        public void CarCreateViewModel_AllProperties_Valid_WithImage()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Image = file,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void CarCreateViewModel_AllProperties_Valid_WithoutImage()
        {
            // Arrange
            var viewModel = new CarEditViewModel
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void CarCreateViewModel_Brand_Required()
        {
            // Arrange
            var viewModel = new CarEditViewModel
            {
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Brand field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void CarCreateViewModel_Brand_Length()
        {
            // Arrange
            var viewModel = new CarEditViewModel
            {
                Brand="s",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Brand must be between 3 and 50 characters.", results[0].ErrorMessage);
        }


        [TestMethod]
        public void CarCreateViewModel_Model_Required()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "BMW",
                EngineType = EngineType.Petrol,
                Image = file,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Model field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_EngineType_Required()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Image = file,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Car_Year_Validation()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Year = 1600,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Year must be between 1900 and 2024.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_PassengerCapacity_Validation()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                Image = file,
                Year = 2009,
                PassengerCapacity = 100,
                Description = "Small description",
                RentalPricePerDay = 50.00m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Passenger Capacity must be between 1 and 16.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void CarCreateViewModel_RentalPricePerDay_Validation()
        {
            // Arrange
            var viewModel = new CarEditViewModel
            {
                Brand = "BMW",
                Model="A3",
                EngineType = EngineType.Hybrid,
                Year = 2021,
                PassengerCapacity = 5,
                Description = "Small description",
                RentalPricePerDay = 5m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Rental Price Per Day must be at least 10.", results[0].ErrorMessage);
        }


        [TestMethod]
        public void CarCreateViewModel_Description_Required()
        {
            // Arrange
            var file = CreateValidFormFile("Test file content", "test_image.jpg");
            var viewModel = new CarEditViewModel
            {
                Brand = "BMW",
                Model = "A3",
                EngineType = EngineType.Hybrid,
                Image = file,
                Year = 2021,
                PassengerCapacity = 5,
                RentalPricePerDay = 76.50m
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Description field is required.", results[0].ErrorMessage);
        }
    }
    
}
