using JewelArchitecture.Examples.SmartCharging.WebApi.Groups;
using JewelArchitecture.Examples.SmartCharging.WebApi.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared
{
    internal class ServiceCollectionFactory
    {
        public static ServiceCollection GetSmartCharging()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddControllers()
                .AddApplicationPart(Assembly.GetAssembly(typeof(GroupController))!)
                .AddControllersAsServices();

            serviceCollection.AddSmartCharging();

            return serviceCollection;
        }
    }
}
