using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentCars.Commons;
using RentCars.Controllers;
using RentCars.Models;
using RentCars.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RentCarsTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<UserManager<RentCarUser>> userManagerMock;
        private readonly Faker<RentCarUser> userFaker;

        public UsersControllerTests()
        {
            userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);

            userFaker = new Faker<RentCarUser>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.UniqueCitinzenshipNumber, f => f.Random.AlphaNumeric(10));
        }

        [Fact]
        public async Task Index_ReturnsViewWithUsers()
        {
            // Arrange
            var users = userFaker.Generate(3);
            userManagerMock.Setup(m => m.GetUsersInRoleAsync(GlobalConstants.UserRoleName)).ReturnsAsync(users);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<RentCarUser>>(viewResult.Model);
            Assert.Equal(users.Count, model.Count());
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToAction_WhenUserExists()
        {
            // Arrange
            var userId = "user-id";
            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(new RentCarUser());
            userManagerMock.Setup(m => m.DeleteAsync(It.IsAny<RentCarUser>())).ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userId = "non-existent-user-id";
            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((RentCarUser)null);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Delete_ReturnsRedirectToAction_Create()
        {
            // Arrange
            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Delete() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create", result.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WithUserSignUpViewModel()
        {
            // Arrange
            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserSignUpViewModel>(viewResult.Model);
        }


        [Fact]
        public async Task Create_ReturnsViewResult_WithModelStateError_WhenModelStateIsInvalid()
        {
            // Arrange
            var userModel = new UserSignUpViewModel
            {
                // Populate the properties with invalid values
                Username = null,
                FirstName = "Test",
                LastName = "User",
                UniqueCitinzenshipNumber = "1234567890",
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Password = "Password123!"
            };

            var controller = new UsersController(userManagerMock.Object);
            controller.ModelState.AddModelError("Username", "The Username field is required."); // Add ModelState error manually

            // Act
            var result = await controller.Create(userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserSignUpViewModel>(viewResult.Model); // Check if the model returned to the view is of type UserSignUpViewModel
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("The Username field is required.", controller.ModelState["Username"].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

        [Fact]
        public async Task Create_ReturnsRedirectToAction_Index_WhenModelStateIsValid()
        {
            // Arrange
            var userModel = new UserSignUpViewModel
            {
                // Populate the properties with valid values
                Username = "testuser",
                FirstName = "Test",
                LastName = "User",
                UniqueCitinzenshipNumber = "1234567890",
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Password = "Password123!"
            };

            userManagerMock.Setup(m => m.CreateAsync(It.IsAny<RentCarUser>(), userModel.Password)).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<RentCarUser>(), GlobalConstants.UserRoleName)).ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Create(userModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            // Check if the ModelState is valid
            Assert.True(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WithModelError_WhenUserExistsWithSameUniqueCitinzenshipNumber()
        {
            // Arrange
            var userModel = new UserSignUpViewModel
            {
                // Populate the properties with valid values
                Username = "testuser",
                FirstName = "Test",
                LastName = "User",
                UniqueCitinzenshipNumber = "1234567890", // Same as an existing user
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Password = "Password123!"
            };

            var existingUser = new RentCarUser
            {
                UniqueCitinzenshipNumber = userModel.UniqueCitinzenshipNumber
            };

            var existingUsers = new List<RentCarUser> { existingUser };
            userManagerMock.Setup(m => m.Users).Returns(existingUsers.AsQueryable());

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Create(userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserSignUpViewModel>(viewResult.Model); // Check if the model returned to the view is of type UserSignUpViewModel
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("A user with the same EGN already exists.", controller.ModelState[string.Empty].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WithModelError_WhenUserExistsWithSameEmail()
        {
            // Arrange
            var userModel = new UserSignUpViewModel
            {
                // Populate the properties with valid values
                Username = "testuser",
                FirstName = "Test",
                LastName = "User",
                UniqueCitinzenshipNumber = "1234567890",
                Email = "test@example.com", // Same as an existing user
                PhoneNumber = "1234567890",
                Password = "Password123!"
            };

            var existingUser = new RentCarUser
            {
                Email = userModel.Email
            };

            var existingUsers = new List<RentCarUser> { existingUser };
            userManagerMock.Setup(m => m.Users).Returns(existingUsers.AsQueryable());

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Create(userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserSignUpViewModel>(viewResult.Model); // Check if the model returned to the view is of type UserSignUpViewModel
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("A user with the same email already exists.", controller.ModelState[string.Empty].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userId = "user-id";
            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((RentCarUser)null);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Edit(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithValidUserEditViewModel()
        {
            // Arrange
            var user = userFaker.Generate();

            var userId = user.Id;
            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Edit(userId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserEditViewModel>(viewResult.Model);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.UserName, model.Username);
            Assert.Equal(user.FirstName, model.FirstName);
            Assert.Equal(user.LastName, model.LastName);
            Assert.Equal(user.UniqueCitinzenshipNumber, model.UniqueCitinzenshipNumber);
            Assert.Equal(user.PhoneNumber, model.PhoneNumber);
            Assert.Equal(user.Email, model.Email);
        }

        [Fact]
        public async Task Edit_ReturnsRedirectToAction_Index_WhenUpdateSucceedsWithValidData()
        {
            // Arrange
            var user = userFaker.Generate();

            var userModel = new UserEditViewModel
            {
                Id = user.Id,
                Username = "updatedUsername",
                FirstName = "updatedFirstName",
                LastName = "updatedLastName",
                UniqueCitinzenshipNumber = "updatedEGN",
                PhoneNumber = "updatedPhoneNumber",
                Email = "updated@example.com",
                Password = "updatedPassword"
            };

            userManagerMock.Setup(m => m.FindByIdAsync(userModel.Id)).ReturnsAsync(user);
            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<RentCarUser>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.RemovePasswordAsync(user)).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.AddPasswordAsync(user, userModel.Password)).ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Edit(userModel.Id, userModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            // Verify if the user properties are updated
            Assert.Equal(userModel.Username, user.UserName);
            Assert.Equal(userModel.FirstName, user.FirstName);
            Assert.Equal(userModel.LastName, user.LastName);
            Assert.Equal(userModel.UniqueCitinzenshipNumber, user.UniqueCitinzenshipNumber);
            Assert.Equal(userModel.PhoneNumber, user.PhoneNumber);
            Assert.Equal(userModel.Email, user.Email);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithInvalidModelState()
        {
            // Arrange
            var user = userFaker.Generate();

            var userId = user.Id;
            var userModel = new UserEditViewModel
            {
                Id = userId,
                // Populate the properties with invalid values
                Username = null, // invalid username
                FirstName = "updatedFirstName",
                LastName = "updatedLastName",
                UniqueCitinzenshipNumber = "updatedEGN",
                PhoneNumber = "updatedPhoneNumber",
                Email = "updated@example.com",
                Password = "updatedPassword"
            };

            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            var controller = new UsersController(userManagerMock.Object);
            controller.ModelState.AddModelError("Username", "The Username field is required.");

            // Act
            var result = await controller.Edit(userId, userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(userModel, viewResult.Model); // Check if the model returned to the view is the same as the one passed to the controller action
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("The Username field is required.", controller.ModelState["Username"].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithInvalidModelStateDueToDuplicateEGN()
        {
            // Arrange
            var user = userFaker.Generate();
            var existingUser = userFaker.Generate(); // Generate another user with the same EGN

            var userId = user.Id;
            var userModel = new UserEditViewModel
            {
                Id = userId,
                Username = "updatedUsername",
                FirstName = "updatedFirstName",
                LastName = "updatedLastName",
                UniqueCitinzenshipNumber = existingUser.UniqueCitinzenshipNumber, // Use an EGN that already exists
                PhoneNumber = "updatedPhoneNumber",
                Email = "updated@example.com",
                Password = "updatedPassword"
            };

            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user); // Ensure that FindByIdAsync returns a valid user
            var existingUsers = new List<RentCarUser> { existingUser };
            userManagerMock.Setup(m => m.Users).Returns(existingUsers.AsQueryable()); // Set up mock to return a user with the same EGN
            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Edit(userId, userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(userModel, viewResult.Model); // Check if the model returned to the view is the same as the one passed to the controller action
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("A user with the same EGN already exists.", controller.ModelState[string.Empty].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithInvalidModelStateDueToDuplicateEmail()
        {
            // Arrange
            var user = userFaker.Generate();
            var existingUser = userFaker.Generate(); // Generate another user with the same email

            var userId = user.Id;
            var userModel = new UserEditViewModel
            {
                Id = userId,
                Username = "updatedUsername",
                FirstName = "updatedFirstName",
                LastName = "updatedLastName",
                UniqueCitinzenshipNumber = "updatedEGN",
                PhoneNumber = "updatedPhoneNumber",
                Email = existingUser.Email, // Use an email that already exists
                Password = "updatedPassword"
            };

            userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user); // Ensure that FindByIdAsync returns a valid user
            var existingUsers = new List<RentCarUser> { existingUser };
            userManagerMock.Setup(m => m.Users).Returns(existingUsers.AsQueryable()); // Set up mock to return a user with the same email
            var controller = new UsersController(userManagerMock.Object);

            // Act
            var result = await controller.Edit(userId, userModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(userModel, viewResult.Model); // Check if the model returned to the view is the same as the one passed to the controller action
            Assert.False(controller.ModelState.IsValid); // Check if the ModelState is invalid
            Assert.Equal("A user with the same email already exists.", controller.ModelState[string.Empty].Errors.First().ErrorMessage); // Check if the expected ModelState error is present
        }

    }
}
