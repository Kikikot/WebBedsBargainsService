using Externals.ConfigSystem;
using Externals.LoggSystem;
using Externals.WebApiSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using WebBedsBargainsService.Dto;
using WebBedsBargainsService.Model;
using WebBedsBargainsService.Service;
using WebBedsBargariansService.UnitTests._Emulation;

namespace WebBedsBargariansService.UnitTests.Service
{
    [TestClass]
    public class BargainsServiceTest
    {
        [TestMethod]
        public void GetAvailabilities_TakesMoreThanOneSecond_ReturnsEmptyList()
        {
            RunTest(
                webApiResponseList: null,
                resultLength: 0,
                forceTimeOut: true
            );
        }

        [TestMethod]
        public void GetAvailabilities_MultipleCombinations_AllwaysOk()
        {
            for (int i = 0; i < 5; i++) // Not more than 5, because it takes to much time
                TestAllPosibilities(i, new List<BSAvailabilityDto>(), 0);
        }

        private void TestAllPosibilities(int length, IEnumerable<BSAvailabilityDto> iList, int acumulatedFinalLength)
        {
            if (length > 0)
                foreach (var type in Enum.GetValues(typeof(DtoTypes)))
                {
                    var dto = EmulateDtos.GetAvailabilityDtoByType((DtoTypes)type);

                    var list = iList.ToList();

                    list.Add(dto);

                    TestAllPosibilities(length - 1, list, acumulatedFinalLength + GetCountLengthOfType((DtoTypes)type));
                }

            RunTest(
                webApiResponseList: iList.ToList(),
                resultLength: acumulatedFinalLength
            );
        }

        private int GetCountLengthOfType(DtoTypes dtoType)
        {
            switch (dtoType)
            {
                case DtoTypes.Perfect:
                case DtoTypes.Huggly:
                    return 1;
            }
            return 0;
        }

        private void RunTest(List<BSAvailabilityDto> webApiResponseList, int resultLength, bool forceTimeOut = false)
        {
            var container = Initiator.Init();
            var webApi = (EmulatedWebApi)container.Resolve<IWebApi>();
            webApi.SetForceTimeOut(forceTimeOut);
            webApi.SetResult(webApiResponseList);

            var service = container.Resolve<IBargainsService>();
            List<BSHotelAvailability> availabities = null;
            bool throwsException = false;

            try
            {
                availabities = service.GetAvailabilities(123, 3);
            }
            catch (Exception)
            {
                throwsException = true;
            }

            Assert.IsFalse(throwsException);
            Assert.IsNotNull(availabities);
            Assert.IsTrue(availabities.Count() == resultLength);
        }
    }
}
