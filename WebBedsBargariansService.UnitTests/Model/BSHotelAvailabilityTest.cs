using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBedsBargainsService.Model;
using System.Linq;

namespace WebBedsBargariansService.UnitTests.Model
{
    [TestClass]
    public class BSHotelAvailabilityTest
    {
        [TestMethod]
        public void AddRate_NewRateAdded_FirstRateFromNullToNotNull()
        {
            var availability = new BSHotelAvailability();

            Assert.IsNull(availability.Rates.FirstOrDefault());

            string boardName = "Board Type";
            decimal finalPrice = 35;

            availability.AddRate(boardName, finalPrice);

            Assert.IsNotNull(availability.Rates.FirstOrDefault());
            Assert.IsTrue(availability.Rates.First().BoardType == boardName);
            Assert.IsTrue(availability.Rates.First().FinalPrice == finalPrice);
        }
    }
}
