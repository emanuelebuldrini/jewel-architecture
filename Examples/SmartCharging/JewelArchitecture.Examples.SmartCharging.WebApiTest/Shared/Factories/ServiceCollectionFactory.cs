using JewelArchitecture.Core.Interface;
using JewelArchitecture.Examples.SmartCharging.WebApi.Groups;
using JewelArchitecture.Examples.SmartCharging.WebApi.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared.Factories;

internal class ServiceCollectionFactory
{
    public static ServiceCollection GetSmartCharging()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddControllers()
            .AddApplicationPart(Assembly.GetAssembly(typeof(GroupController))!)
            .AddControllersAsServices();

        serviceCollection
            .AddJewelArchitecture()
            .AddSmartCharging();

        return serviceCollection;
    }
}
