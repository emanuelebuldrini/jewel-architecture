using JewelArchitecture.Core.Interface;
using JewelArchitecture.Examples.SmartCharging.Interface.Groups;
using JewelArchitecture.Examples.SmartCharging.Interface.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JewelArchitecture.Examples.SmartCharging.Test.Shared.Factories;

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
