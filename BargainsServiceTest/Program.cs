using Unity;
using WebBedsBargainsService.Service;
using WebBedsBargainsService.Unity;

namespace BargainsServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WebBedsBargariansService.UnitTests.Service.BargainsServiceTest().GetAvailabilities_GetsOnePerfect_ReturnsListLengthOne();

            IUnityContainer container = InitContainer();

            UseService(container.Resolve<IBargainsService>());
        }

        private static void UseService(IBargainsService service)
        {
            var availability = service.GetAvailabilities(1419, 2);
        }

        private static IUnityContainer InitContainer()
        {
            IUnityContainer container = new UnityContainer();

            Registrator.Register(container);

            return container;
        }
    }
}
