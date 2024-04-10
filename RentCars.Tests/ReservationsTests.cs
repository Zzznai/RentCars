using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using RentCars.Commons.Enums;
using RentCars.Controllers;
using RentCars.Data;
using RentCars.Models;
using System;

namespace RentCars.Tests
{
    public class CarTests
    {
        [Fact]
        public void AllReservations_ReturnsViewWithReservations()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RentCarDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Populate the database with some sample reservations
            using (var dbContext = new RentCarDbContext(options))
            {
                dbContext.Reservations.AddRange(
                    new List<Reservation>
                    {
                        new Reservation { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(3), Car = new Car { Brand = "Brand1", Model="Model1", EngineType = EngineType.Diesel, ImageUrl=string.Empty, Year=2022, Description="A new test car", PassengerCapacity=8, RentalPricePerDay=20.50M}, User = new RentCarUser { UserName = "User1", FirstName = "Test Firstname", LastName="Test LastName" , Email = "test@abv.bg", PhoneNumber="08888888888", UniqueCitinzenshipNumber="1010101010"} },
                        new Reservation { Id = 2, StartDate = DateTime.Now.Date.AddDays(1), EndDate = DateTime.Now.Date.AddDays(5), Car = new Car { Id = 2, Brand = "Brand2" }, User = new RentCarUser { Id = "2", UserName = "User2" } }
                    });
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = new RentCarDbContext(options))
            {
                var controller = new AdminController(dbContext);

                var result = controller.AllReservations() as ViewResult;

                // Assert
                Assert.NotNull(result);

                var model = result.Model as List<Reservation>;
                Assert.NotNull(model);
                Assert.Equal(2, model.Count); // Ensure that all reservations are retrieved

                // You can add more assertions to ensure correct ordering or specific reservations
                Assert.Equal(1, model[0].Id); // Ensure that the first reservation has the expected ID
                Assert.Equal(2, model[1].Id); // Ensure that the second reservation has the expected ID
            }
        }
    }
}