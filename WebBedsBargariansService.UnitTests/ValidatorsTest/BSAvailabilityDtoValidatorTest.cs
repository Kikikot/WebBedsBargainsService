using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBedsBargariansService.UnitTests._Emulation;
using WebBedsBargainsService.Dto;
using System;
using Unity;
using WebBedsBargainsService.Base;

namespace WebBedsBargariansService.UnitTests.ValidatorsTest
{
    [TestClass]
    public class BSAvailabilityDtoValidatorTest
    {
        [TestMethod]
        public void IsAcceptedResponse_PerfectEachRateType_ReturnTrue()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            foreach (var type in Enum.GetValues(typeof(AvailabilityRateType)))
            {
                var dto = EmulateDtos.GetAvailabilityDtoByType(DtoTypes.Perfect, (AvailabilityRateType)type);

                Assert.IsTrue(validator.IsAcceptedResponse(dto));
            }
        }

        [TestMethod]
        public void IsAcceptedResponse_BadHotelName_ReturnFalse()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            var dto = EmulateDtos.GetAvailabilityDtoByType(DtoTypes.BadName);

            Assert.IsFalse(validator.IsAcceptedResponse(dto));
        }

        [TestMethod]
        public void IsAcceptedResponse_BadBoardPrice_Valid_ReturnTrue()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            var dto = EmulateDtos.GetAvailabilityDtoByType(DtoTypes.Huggly);

            Assert.IsTrue(validator.IsAcceptedResponse(dto));
        }

        [TestMethod]
        public void IsAcceptedResponse_BadBoardPrice_NotValid_ReturnFalse()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            var dto = EmulateDtos.GetAvailabilityDtoByType(DtoTypes.BadPrice);

            Assert.IsFalse(validator.IsAcceptedResponse(dto));
        }

        [TestMethod]
        public void IsCorrectRate_CorrectPrice_ReturnTrue()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            foreach (var type in Enum.GetValues(typeof(AvailabilityRateType)))
            {
                var dto = EmulateDtos.GetRate("Board Type 1", (AvailabilityRateType)type);

                Assert.IsTrue(validator.IsCorrectRate(dto));
            }
        }

        [TestMethod]
        public void IsCorrectRate_BadPrice_ReturnFalse()
        {
            var container = TestRegistrator.Init();
            var validator = container.Resolve<IBSAvailabilityDtoValidator>();

            foreach (var type in Enum.GetValues(typeof(AvailabilityRateType)))
            {
                var dto = EmulateDtos.GetRate("Board Type 1", (AvailabilityRateType)type, false);

                Assert.IsFalse(validator.IsCorrectRate(dto));
            }
        }
    }
}
