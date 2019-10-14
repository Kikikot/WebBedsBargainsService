using Externals.ConfigSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using WebBedsBargainsService.Base;
using WebBedsBargariansService.UnitTests._Emulation;

namespace WebBedsBargariansService.UnitTests.UrlProvider
{
    [TestClass]
    public class BargainsUrlProviderTest
    {
        [TestMethod]
        public void GetAvailabilitiesUrl_KeysInitiated_UrlNotNullAndNotException()
        {
            var container = TestRegistrator.Init();
            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            config.Set("BS:BargainsEndPoint", "https://bargains.endpoint.net");
            config.Set("BS:BargainsUserCode", "personalSecurityCode");

            var urlProvider = container.Resolve<IBargainsUrlProvider>();

            string url = null;
            bool throwsException = false;

            try
            {
                url = urlProvider.GetAvailabilitiesUrl(123, 4);
            }
            catch (Exception e)
            {
                throwsException = true;
            }

            Assert.IsFalse(throwsException);
            Assert.IsNotNull(url);
        }

        [TestMethod]
        public void GetAvailabilitiesUrl_FirstKeyNotInitiated_UrlNullAndException()
        {
            var container = TestRegistrator.Init();
            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            config.Set("BS:BargainsUserCode", "personalSecurityCode");

            var urlProvider = container.Resolve<IBargainsUrlProvider>();

            string url = null;
            bool throwsException = false;

            try
            {
                url = urlProvider.GetAvailabilitiesUrl(123, 4);
            }
            catch (Exception e)
            {
                throwsException = true;
            }

            Assert.IsTrue(throwsException);
            Assert.IsNull(url);
        }

        [TestMethod]
        public void GetAvailabilitiesUrl_SecondtKeyNotInitiated_UrlNullAndException()
        {
            var container = TestRegistrator.Init();
            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            config.Set("BS:BargainsEndPoint", "https://bargains.endpoint.net");

            var urlProvider = container.Resolve<IBargainsUrlProvider>();

            string url = null;
            bool throwsException = false;

            try
            {
                url = urlProvider.GetAvailabilitiesUrl(123, 4);
            }
            catch (Exception e)
            {
                throwsException = true;
            }

            Assert.IsTrue(throwsException);
            Assert.IsNull(url);
        }
    }
}
