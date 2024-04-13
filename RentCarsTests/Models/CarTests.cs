using RentCars.Commons.Enums;
using RentCars.Models;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Tests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Car_AllProperties_Valid()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Petrol,
                ImageUrl = "http://tests//test_image.jpg",
                Year = 2021,
                PassengerCapacity = 5,
                RentalPricePerDay = 50.00m,
                Description = "Small description",
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Car_Brand_Required()
        {
            // Arrange
            var car = new Car { Model = "Tesla", EngineType = EngineType.Hybrid, ImageUrl = "http://tests//test_image.jpg", Year = 2021, PassengerCapacity = 5, RentalPricePerDay = 50.00m, Description = "Testing is fun!" };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Brand field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_Brand_ValidLength()
        {
            // Arrange
            var car = new Car { Brand = "A", Model = "TestModel", EngineType = EngineType.Hybrid, ImageUrl = "http://tests//test_image.jpg", Year = 2021, PassengerCapacity = 5, RentalPricePerDay = 50.00m, Description = "Testing is fun!" };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Brand must be between 3 and 50 characters.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_Model_Required()
        {
            // Arrange
            var car = new Car { Brand = "Toyota", EngineType = EngineType.Hybrid, ImageUrl = "http://tests//test_image.jpg", Year = 2021, PassengerCapacity = 5, RentalPricePerDay = 50.00m, Description = "Testing is fun!" };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Model field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_EngineType_Required()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                ImageUrl = "http://tests//test_image.jpg",
                Year = 2021,
                PassengerCapacity = 5,
                RentalPricePerDay = 50.00m,
                Description = "Small description",
                EngineType= EngineType.Electric,
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count); 
        }

        [TestMethod]
        public void Car_ImageUrl_Required()
        {
            // Arrange
            var car = new Car
            {
                Brand = "BMW",
                Model = "A3",
                EngineType = EngineType.Petrol,
                Year = 2021,
                PassengerCapacity = 5,
                RentalPricePerDay = 50.00m,
                Description = "Small description",
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Image Url field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_Year_Validation()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Diesel,
                ImageUrl = "http://tests//test_image.jpg",
                PassengerCapacity = 5,
                RentalPricePerDay = 50.00m,
                Description = "Small description",
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The Year must be between 1900 and 2024.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void Car_PassengerCapacity_Validation()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Diesel,
                ImageUrl = "http://tests//test_image.jpg",
                Year = 2016,
                PassengerCapacity=-9,
                RentalPricePerDay = 50.00m,
                Description = "Small description",
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The Passenger Capacity must be between 1 and 16.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void Car_Description_Required()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Diesel,
                ImageUrl = "http://tests//test_image.jpg",
                Year = 2016,
                PassengerCapacity = 9,
                RentalPricePerDay = 50.00m,
            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Description field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Car_RentalPricePerDay_Validation()
        {
            // Arrange
            var car = new Car
            {
                Brand = "Toyota",
                Model = "Camry",
                EngineType = EngineType.Diesel,
                ImageUrl = "http://tests//test_image.jpg",
                Year = 2016,
                PassengerCapacity = 9,
                RentalPricePerDay = 0m,
                Description = "Small description",

            };

            // Act
            var context = new ValidationContext(car, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(car, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Rental Price Per Day must be between 10 and 10000.", results[0].ErrorMessage);
        }

    }
}
