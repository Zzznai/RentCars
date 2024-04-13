using Microsoft.AspNetCore.Http;
using RentCars.Commons.Enums;
using RentCars.Models;
using RentCars.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Tests
{
    [TestClass]
    public class UserSigninViewModelTests
    {
        [TestMethod]
        public void UserSigninViewModel_AllProperties_Valid()
        {
            // Arrange
            var viewModel = new UserSignInViewModel
            {
                Username = "Wintaa",
                Password = "password05@",
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
        public void UserSigninViewModel_Username_Required()
        {
            // Arrange
            var viewModel = new UserSignInViewModel
            {
                Password = "password05@",
            };

            //Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Username field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserSigninViewModel_Password_Required()
        {
            // Arrange
            var viewModel = new UserSignInViewModel
            {
                Username = "Wintaa",
            };

            //Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Password field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserSigninViewModel_All_Required()
        {
            // Arrange
            var viewModel = new UserSignInViewModel
            {
            };

            //Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The Username field is required.", results[0].ErrorMessage);
            Assert.AreEqual("The Password field is required.", results[1].ErrorMessage);
        }
    }
}