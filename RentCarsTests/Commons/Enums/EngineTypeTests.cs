using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentCars.Commons.Enums;

namespace RentCarsTests.Enums
{
    /// <summary>
    /// Unit tests for the EngineType enum
    /// </summary>
    [TestClass]
    public class EngineTypeTests
    {
        [TestMethod]
        public void EngineType_Enum_Should_Have_Correct_Values()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(0, (int)EngineType.Diesel);
            Assert.AreEqual(1, (int)EngineType.Petrol);
            Assert.AreEqual(2, (int)EngineType.Electric);
            Assert.AreEqual(3, (int)EngineType.Hybrid);
        }
    }
}
