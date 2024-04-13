using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RentCars.Models;
using RentCars.Commons.Enums;

namespace RentCarsTests.Models
{
    /// <summary>
    /// Unit tests for the Reservation model
    /// </summary>
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void Reservation_AllProperties_Valid()
        {
            var reservation = new Reservation
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                RentalSum = 100.00m,
                Status = ReservationStatus.Denied,
                Car = new Car(),
                User=new RentCarUser(),
            };

            // Act
            var context = new ValidationContext(reservation, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reservation, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Reservation_user_Required()
        {
            // Arrange
            var reservation = new Reservation
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                RentalSum = 100.00m,
                Status = ReservationStatus.Denied,
                Car = new Car(),
            };

            // Act
            var context = new ValidationContext(reservation, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reservation, context, results, validateAllProperties: true);

            // Assert
            Assert.IsTrue(!isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The User field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Reservation_Car_Required()
        {
            // Arrange
            var reservation = new Reservation
            {
                StartDate = DateTime.Now,
                EndDate= DateTime.Now.AddDays(3),
                RentalSum = 4500.00m,
                Status = ReservationStatus.Waiting,
                User = new RentCarUser(),
            };

            // Act
            var context = new ValidationContext(reservation, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(reservation, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count); 
            Assert.AreEqual("The Car field is required.", results[0].ErrorMessage);
        }

    }
}
