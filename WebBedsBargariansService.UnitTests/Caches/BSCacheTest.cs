using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBedsBargainsService.Base;
using Unity;
using WebBedsBargainsService.Model;
using System.Collections.Generic;
using Externals.ConfigSystem;
using WebBedsBargariansService.UnitTests._Emulation;

namespace WebBedsBargariansService.UnitTests.Caches
{
    [TestClass]
    public class BSCacheTest
    {
        [TestMethod]
        public void GetBSHotelAvailabilities_CacheActive_NotExistingKey_ReturnsNull()
        {
            var container = TestRegistrator.Init();
            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            //config.Set("BS:BargainsEndPoint", "https://webbedsdevtest.azurewebsites.net");
            //config.Set("BS:BargainsUserCode", "aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==");
            config.Set("BS:CacheMinutes", "5");

            var cache = container.Resolve<IBSCache>();

            var result = cache.GetBSHotelAvailabilities(1, 1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBSHotelAvailabilities_CacheNotActive_NotExistingKey_ReturnsNull()
        {
            var container = TestRegistrator.Init();

            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();

            var cache = container.Resolve<IBSCache>();

            var result = cache.GetBSHotelAvailabilities(2, 2);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBSHotelAvailabilities_CacheActive_ExistingKey_ReturnsObject()
        {
            var container = TestRegistrator.Init();

            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            config.Set("BS:CacheMinutes", "5");

            var cache = container.Resolve<IBSCache>();

            cache.SetBSHotelAvailabilities(3, 3, new List<BSHotelAvailability>());

            var result = cache.GetBSHotelAvailabilities(3, 3);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetBSHotelAvailabilities_CacheNotActive_ExistingKey_ReturnsNull()
        {
            var container = TestRegistrator.Init();

            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();

            var cache = container.Resolve<IBSCache>();

            cache.SetBSHotelAvailabilities(4, 4, new List<BSHotelAvailability>());

            var result = cache.GetBSHotelAvailabilities(4, 4);

            Assert.IsNull(result);
        }
    }
}
