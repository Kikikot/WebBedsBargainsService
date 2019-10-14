using Externals.ConfigSystem;
using Externals.WebApiSystem;
using Unity;
using WebBedsBargainsService.Unity;

namespace WebBedsBargariansService.UnitTests._Emulation
{
    public static class TestRegistrator
    {
        public static IUnityContainer GetEmptyContainer()
        {
            return new UnityContainer();
        }

        public static IUnityContainer Init(IUnityContainer container = null)
        {
            if (container == null)
                container = GetEmptyContainer();

            RegisterType<IConfig, EmulatedConfig>(container);
            RegisterType<IWebApi, EmulatedWebApi>(container);

            Registrator.Register(container);

            return container;
        }

        private static void RegisterType<I, C>(IUnityContainer container) where C : I
        {
            if (!container.IsRegistered<I>())
                container.RegisterType<I, C>();
        }
    }
}
