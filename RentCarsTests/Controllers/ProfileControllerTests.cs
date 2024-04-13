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
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RentCars.Controllers
{
    public class ProfileControllerTests
    {
        private readonly Faker<RentCarUser> userFaker;

        public ProfileControllerTests()
        {
            userFaker = new Faker<RentCarUser>()
                .RuleFor(u => u.Id, f => f.Random.Guid().ToString())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.UniqueCitinzenshipNumber, f => f.Random.AlphaNumeric(10));
        }

        [Fact]
        public async Task Index_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userId");
            userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync((RentCarUser)null);

            var controller = new ProfileController(userManagerMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithValidUserProfileModel()
        {
            // Arrange
            var user = userFaker.Generate();

            var userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userId");
            userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(user);

            var controller = new ProfileController(userManagerMock.Object);

            // Act
            var result = await controller.Index();

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
        public async Task Index_Post_ReturnsViewResult_WithInvalidModelState()
        {
            // Arrange
            var userProfileModel = new UserEditViewModel();
            var controller = new ProfileController(null);

            controller.ModelState.AddModelError("errorKey", "errorMessage");

            // Act
            var result = await controller.Index(userProfileModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(userProfileModel, viewResult.Model);

            // Verify ModelState contains errors
            Assert.False(controller.ModelState.IsValid);
            Assert.Contains("errorKey", controller.ModelState.Keys);
            var modelStateEntry = controller.ModelState["errorKey"];
            Assert.Equal("errorMessage", modelStateEntry.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Index_Post_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userId");
            userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync((RentCarUser)null);

            var controller = new ProfileController(userManagerMock.Object);

            // Act
            var result = await controller.Index(new UserEditViewModel());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Index_Post_ReturnsRedirectToActionResult_WhenUpdateSucceedsWithPasswordChange()
        {
            // Arrange
            var userProfileModel = new UserEditViewModel
            {
                Id = "userId",
                Username = "testUser",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "123456789",
                Email = "test@example.com",
                Password = "NewPassword123"
            };

            var user = userFaker.Generate();
            user.Id = "userId";

            var userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userId");
            userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(user);
            userManagerMock.Setup(m => m.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.RemovePasswordAsync(user)).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.AddPasswordAsync(user, userProfileModel.Password)).ReturnsAsync(IdentityResult.Success);

            var controller = new ProfileController(userManagerMock.Object);

            // Act
            var result = await controller.Index(userProfileModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            // Check if user data is updated correctly
            Assert.Equal(userProfileModel.Username, user.UserName);
            Assert.Equal(userProfileModel.FirstName, user.FirstName);
            Assert.Equal(userProfileModel.LastName, user.LastName);
            Assert.Equal(userProfileModel.UniqueCitinzenshipNumber, user.UniqueCitinzenshipNumber);
            Assert.Equal(userProfileModel.PhoneNumber, user.PhoneNumber);
            Assert.Equal(userProfileModel.Email, user.Email);
            userManagerMock.Verify(m => m.RemovePasswordAsync(user), Times.Once);
            userManagerMock.Verify(m => m.AddPasswordAsync(user, userProfileModel.Password), Times.Once);
        }

        [Fact]
        public async Task Index_Post_ReturnsRedirectToActionResult_WhenUpdateSucceedsWithoutPasswordChange()
        {
            // Arrange
            var userProfileModel = new UserEditViewModel
            {
                Id = "userId",
                Username = "testUser",
                FirstName = "John",
                LastName = "Doe",
                UniqueCitinzenshipNumber = "1234567890",
                PhoneNumber = "123456789",
                Email = "test@example.com"
            };

            var user = userFaker.Generate();
            user.Id = "userId";

            var userManagerMock = new Mock<UserManager<RentCarUser>>(Mock.Of<IUserStore<RentCarUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userId");
            userManagerMock.Setup(m => m.FindByIdAsync("userId")).ReturnsAsync(user);
            userManagerMock.Setup(m => m.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var controller = new ProfileController(userManagerMock.Object);

            // Act
            var result = await controller.Index(userProfileModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            // Check if user data is updated correctly
            Assert.Equal(userProfileModel.Username, user.UserName);
            Assert.Equal(userProfileModel.FirstName, user.FirstName);
            Assert.Equal(userProfileModel.LastName, user.LastName);
            Assert.Equal(userProfileModel.UniqueCitinzenshipNumber, user.UniqueCitinzenshipNumber);
            Assert.Equal(userProfileModel.PhoneNumber, user.PhoneNumber);
            Assert.Equal(userProfileModel.Email, user.Email);
        }
    }
}
