using Microsoft.AspNetCore.Http;
using RentCars.Commons.Enums;
using RentCars.Models;
using RentCars.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace RentCarsTests.ViewModels
{
    /// <summary>
    /// Unit tests for the UserSignUpViewModel
    /// </summary>
    [TestClass]
    public class UserSignUpViewModelTests
    {
        [TestMethod]
        public void UserSignUpViewModel_AllProperties_Valid()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "I.Ivanova",
                FirstName="Ivanka",
                LastName="Ivanova",
                UniqueCitinzenshipNumber="9056789035",
                PhoneNumber="0897456705",
                Email="eee_ir@gmail.com",
                Password="Nabor05#",
                ConfirmPassword = "Nabor05#"
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
            var viewModel = new UserSignUpViewModel
            {
                FirstName = "Ivanka",
                LastName = "Ivanova",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "st_teacher@gmail.com",
                Password = "Nabor05#",
                ConfirmPassword = "Nabor05#"
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
        public void UserSignUpViewModel_Username_Length()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "ii",
                FirstName = "Ivanka",
                LastName = "Ivanova",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "thebest_teacher@gmail.com",
                Password = "Nabor05#",
                ConfirmPassword = "Nabor05#"
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
        public void UserSignUpViewModel_FirstName_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username="User_34",
                LastName = "Rumelov",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_FirstName_Length()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "N",
                LastName = "Rumelov",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_LastName_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_LastName_Length()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "R",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_UniqueCitizenshipNumber_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_UniqueCitizenshipNumber_Format()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "123456789A",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_PhoneNumber_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_PhoneNumber_Format()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "A",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_Email_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Password = "23wed!",
                ConfirmPassword = "23wed!"
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
        public void UserSignUpViewModel_Email_Format()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "I.Ivanova",
                FirstName = "Ivanka",
                LastName = "Ivanova",
                UniqueCitinzenshipNumber = "9056789035",
                PhoneNumber = "0897456705",
                Email = "eee_ir",
                Password = "Nabor05#",
                ConfirmPassword = "Nabor05#"
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.AreEqual("Invalid Email Address.", results[0].ErrorMessage);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void UserSignUpViewModel_Password_ConfirmPassword_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The Password field is required.", results[0].ErrorMessage);
            Assert.AreEqual("The Confirm Password field is required.", results[1].ErrorMessage);
        }

        [TestMethod]
        public void UserSignUpViewModel_Password_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                ConfirmPassword = "Nabor05#"
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The Password field is required.", results[0].ErrorMessage);
            Assert.AreEqual("The Confirm Password must match the Password.", results[1].ErrorMessage);
        }

        [TestMethod]
        public void UserSignUpViewModel_ConfirmPassword_Required()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "Nabor05#"
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Confirm Password field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserSignUpViewModel_Password_MatchConfirmPassword()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed"
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Confirm Password must match the Password.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void UserSignUpViewModel_ConfirmPassword_MatchPassword()
        {
            // Arrange
            var viewModel = new UserSignUpViewModel
            {
                Username = "User_34",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "0897456705",
                Email = "st_naum@yahoo.com",
                Password = "23wed!",
                ConfirmPassword = "23wed"
            };

            // Act
            var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Confirm Password must match the Password.", results[0].ErrorMessage);
        }



    }
}
