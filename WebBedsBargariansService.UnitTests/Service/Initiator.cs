using Externals.ConfigSystem;
using Unity;
using WebBedsBargariansService.UnitTests._Emulation;

namespace WebBedsBargariansService.UnitTests.Service
{
    public static class Initiator
    {
        public static IUnityContainer Init()
        {
            var container = TestRegistrator.Init();
            var config = (EmulatedConfig)container.Resolve<IConfig>();
            config.Clean();
            config.Set("BS:BargainsEndPoint", "https://bargains.endpoint.net");
            config.Set("BS:BargainsUserCode", "personalSecurityCode");

            return container;
        }
    }
}
