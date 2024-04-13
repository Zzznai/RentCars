using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentCars.Commons.Enums;

namespace RentCarsTests.Enums
{
    /// <summary>
    /// Unit tests for the ReservationStatus enum
    /// </summary>
    [TestClass]
    public class ReservationStatusTests
    {
        [TestMethod]
        public void ReservationStatus_Enum_Should_Have_Correct_Values()
        {

            // Arrange

            // Act

            // Assert
            Assert.AreEqual(0, (int)ReservationStatus.Confirmed);
            Assert.AreEqual(1, (int)ReservationStatus.Waiting);
            Assert.AreEqual(2, (int)ReservationStatus.Denied);
        }
    }
}
