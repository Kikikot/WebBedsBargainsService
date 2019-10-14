using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBedsBargainsService.Model;
using WebBedsBargariansService.UnitTests._Emulation;
using System.Linq;
using WebBedsBargainsService.Dto;
using System;
using Unity;
using WebBedsBargainsService.Base;

namespace WebBedsBargariansService.UnitTests.TranslatorTest
{
    [TestClass]
    public class BSAvailabilityTranslatorTest
    {
        private const int NIGHTS = 4;

        [TestMethod]
        public void Translate_PerfectEachRateType_EquivalentFields()
        {
            var container = TestRegistrator.Init();
            var translator = container.Resolve<IBSAvailabilityTranslator>();

            foreach (var type in Enum.GetValues(typeof(AvailabilityRateType)))
            {
                var calculatedItem = translator.Translate(EmulateDtos.GetAvailabilityDtoByType(DtoTypes.Perfect, (AvailabilityRateType)type), NIGHTS);

                var espectedItem = EmulateModels.GetPerfect((AvailabilityRateType)type, NIGHTS);

                CheckNotNullAsserts(calculatedItem, espectedItem);
            }
        }

        [TestMethod]
        public void Translate_BadHotelName_ReturnNull()
        {
            var container = TestRegistrator.Init();
            var translator = container.Resolve<IBSAvailabilityTranslator>();

            var calculatedItem = translator.Translate(EmulateDtos.GetAvailabilityDtoByType(DtoTypes.BadName), NIGHTS);

            Assert.IsNull(calculatedItem);
        }

        [TestMethod]
        public void Translate_BadBoardPrice_Valid_EquivalentFields()
        {
            var container = TestRegistrator.Init();
            var translator = container.Resolve<IBSAvailabilityTranslator>();

            var calculatedItem = translator.Translate(EmulateDtos.GetAvailabilityDtoByType(DtoTypes.Huggly), NIGHTS);

            var espectedItem = EmulateModels.GeValidWithBadBoardPrice(NIGHTS);

            CheckNotNullAsserts(calculatedItem, espectedItem);
        }

        [TestMethod]
        public void Translate_BadBoardPrice_NotValid_ReturnNull()
        {
            var container = TestRegistrator.Init();
            var translator = container.Resolve<IBSAvailabilityTranslator>();

            var dto = EmulateDtos.GetAvailabilityDtoByType(DtoTypes.BadPrice);

            Assert.IsNull(translator.Translate(dto, NIGHTS));
        }

        private void CheckNotNullAsserts(BSHotelAvailability calculatedItem, BSHotelAvailability espectedItem)
        {
            Assert.IsTrue(calculatedItem?.Rates?.FirstOrDefault() != null);
            Assert.IsTrue(calculatedItem.Rates.Count() == 1);
            Assert.IsTrue(calculatedItem.Name == espectedItem.Name);
            Assert.IsTrue(calculatedItem.Rates.First().BoardType == espectedItem.Rates.First().BoardType);
            Assert.IsTrue(calculatedItem.Rates.First().FinalPrice == espectedItem.Rates.First().FinalPrice);
        }
    }
}
