using RentCars.Commons.Enums;
using RentCars.Models;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Tests
{
    /// <summary>
    /// Unit tests for the RentCarUser model
    /// </summary>
    [TestClass]
    public class RentCarUserTests
    {
        [TestMethod]
        public void RentCarUser_AllProperties_Valid()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "Naim",
                LastName = "Abaz",
                UniqueCitinzenshipNumber = "1234567895",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void RentCarUser_FirstName_Required()
        {
            // Arrange
            var user = new RentCarUser
            {
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The First Name field is required.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void RentCarUser_FirstName_Length()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "J", 
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The First Name must be between 3 and 50 characters.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void RentCarUser_LastName_Required()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "John",
                UniqueCitinzenshipNumber = "1234567890",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The Last Name field is required.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void RentCarUser_LastName_Length()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "John",
                LastName = "D", 
                UniqueCitinzenshipNumber = "1234567890",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The Last Name must be between 3 and 50 characters.", results[0].ErrorMessage); 
        }

        [TestMethod]
        public void RentCarUser_UniqueCitinzenshipNumber_Required()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "John",
                LastName = "Doe",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Unique Citizenship Number field is required.", results[0].ErrorMessage);
        }
        [TestMethod]
        public void RentCarUser_UniqueCitinzenshipNumber_Length()
        {
            // Arrange
            var user = new RentCarUser
            {
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890A",
            };

            // Act
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid); 
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Unique Citizenship Number must be a 10-digit number.", results[0].ErrorMessage);
        }


    }
}
