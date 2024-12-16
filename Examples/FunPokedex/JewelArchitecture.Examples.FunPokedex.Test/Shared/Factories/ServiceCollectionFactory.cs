using JewelArchitecture.Core.Interface.Extensions;
using Microsoft.Extensions.DependencyInjection;
using JewelArchitecture.Examples.FunPokedex.Interface.Pokemon;
using JewelArchitecture.Examples.FunPokedex.Interface.Shared;
using System.Reflection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared.Factories;

internal class ServiceCollectionFactory
{
    public static ServiceCollection GetPokedex()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddControllers()
            .AddApplicationPart(Assembly.GetAssembly(typeof(PokemonController))!)
            .AddControllersAsServices();       

        serviceCollection
            .AddJewelArchitecture()
            .AddPokedex();       

        return serviceCollection;
    }
}
