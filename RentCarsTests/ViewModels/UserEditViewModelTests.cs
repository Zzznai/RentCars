using Microsoft.AspNetCore.Http;
using RentCars.Commons.Enums;
using RentCars.Models;
using RentCars.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace RentCarsTests.ViewModels
{
    /// <summary>
    /// Unit tests for the UserEditViewModel
    /// </summary>
    [TestClass]
    public class UserEditViewModelTests
    {
        [TestMethod]
        public void UserEditViewModelTests_AllProperties_Valid_WithPassword()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Poli",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
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
        public void UserEditViewModelTests_AllProperties_Valid_WithoutPassword()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Poli",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
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
        public void UserSignUpViewModel_Username_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                FirstName = "Poli",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Username field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_Username_Length()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Po",
                FirstName = "Poli",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100!",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Username must be at least 3 characters.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_FirstName_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100!",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The First Name field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_FirstName_Length()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "q",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The First Name must be between 3 and 50 characters.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_LastName_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Last Name field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_LastName_Length()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "M",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Last Name must be between 3 and 50 characters.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_UniqueCitinzenshipNumber_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "Medeva",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Unique Citizenship Number field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_UniqueCitinzenshipNumber_Format()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "123456789",
                PhoneNumber = "0897456705",
                Email = "pepa_a@gmail.com",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Unique Citizenship Number must be a 10-digit number.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_PhoneNumber_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Phone Number field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_PhoneNumber_Format()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "text",
                Email = "pepa_a@gmail.com",
                Password = "Pepa_100",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The PhoneNumber field is not a valid phone number.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_Email_Required()
        {
            // Arrange
            var viewModel = new UserEditViewModel
            {
                Username = "Pepa",
                FirstName = "Petar",
                LastName = "Medeva",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Email field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserEditViewModel_Email_Format()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "Madison",
                FirstName = "Madison",
                LastName = "Beer",
                UniqueCitinzenshipNumber = "0895674321",
                PhoneNumber = "0897456705",
                Email="e_@",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.AreEqual("Invalid Email Address.", results[0].ErrorMessage);
            Assert.IsFalse(isValid);
        }


    }
}
