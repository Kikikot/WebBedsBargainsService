using Externals.CacheSystem;
using Externals.ConfigSystem;
using Externals.LoggSystem;
using Externals.WebApiSystem;
using Unity;
using WebBedsBargainsService.Base;
using WebBedsBargainsService.Caches;
using WebBedsBargainsService.Loggers;
using WebBedsBargainsService.Service;
using WebBedsBargainsService.Translators;
using WebBedsBargainsService.UrlProviders;
using WebBedsBargainsService.Validators;

namespace WebBedsBargainsService.Unity
{
    public static class Registrator
    {
        private static IUnityContainer _container { get; set; }
        public static IUnityContainer Container { get { return _container; } }

        public static void Register(IUnityContainer container)
        {
            _container = container;

            Register<IWebApi, WebApi>();
            Register<IConfig, Config>();
            Register<ILogger, BSLogger>();
            Register<ICache, Cache>();
            Register<IBSCache, BSCache>();
            Register<IBSAvailabilityDtoValidator, BSAvailabilityDtoValidator>();
            Register<IBSAvailabilityTranslator, BSAvailabilityTranslator>();
            Register<IBargainsUrlProvider, BargainsUrlProvider>();
            Register<IBargainsService, BargainsService>();
        }

        private static void Register<I, C>() where C : I
        {
            if (!Container.IsRegistered<I>())
                Container.RegisterType<I, C>();
        }
    }
}
