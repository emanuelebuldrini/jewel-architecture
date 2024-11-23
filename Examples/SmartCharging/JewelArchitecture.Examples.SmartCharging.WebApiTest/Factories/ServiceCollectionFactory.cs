using JewelArchitecture.Examples.SmartCharging.WebApi.Controllers;
using JewelArchitecture.Examples.SmartCharging.WebApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Factories
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
