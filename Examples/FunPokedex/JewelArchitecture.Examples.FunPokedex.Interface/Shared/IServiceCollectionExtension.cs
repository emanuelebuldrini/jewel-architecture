using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.ApplicationServices;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.UseCases;
using JewelArchitecture.Examples.FunPokedex.Application.Shared.FunTranslations;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.FunTranslations;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.Pokeapi;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.Caching;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.Resilience;

namespace JewelArchitecture.Examples.FunPokedex.Interface.Shared;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddPokedex(this IServiceCollection serviceCollection)
    {
        // The following services depend on HttpClient:
        // They should be transient services to avoid trapping HttpClient's lifetime.
        serviceCollection.AddTransient<PokemonService>();
        serviceCollection.AddTransient<PokemonTranslatedCase>();

        // Add decorators
        serviceCollection.Decorate(typeof(IQueryHandler<,>), typeof(QueryHandlerResilienceDecorator<,>));

        // Used to cache responses from the external APIs.
        serviceCollection.AddSingleton<IStreamCachingService, FileStreamCachingService>();
        
        return serviceCollection;
    }

    // Add external API client helpers

    public static IServiceCollection AddPokeapiClient(this IServiceCollection serviceCollection,
        IConfigurationSection pokeapiConfigSection)
    {
        serviceCollection.AddTransient<IPokeapiClient, PokeapiClient>();

        serviceCollection.AddHttpClient<PokeapiClient>();
        serviceCollection.Configure<PokeapiOptions>(pokeapiConfigSection)
            .AddOptionsWithValidateOnStart<PokeapiOptions>();

        return serviceCollection;
    }

    public static IServiceCollection AddFuntranslationsClient(this IServiceCollection serviceCollection,
       IConfigurationSection funTranslationsConfigSection)
    {
        serviceCollection.AddTransient<IFuntranslationsClient, FuntranslationsClient>();

        serviceCollection.AddHttpClient<FuntranslationsClient>();
        serviceCollection.Configure<FuntranslationsApiOptions>(funTranslationsConfigSection)
            .AddOptionsWithValidateOnStart<FuntranslationsApiOptions>();

        return serviceCollection;
    }

    public static IServiceCollection AddRetryPolicyOptions(this IServiceCollection serviceCollection,
        IConfigurationSection retryPolicyConfigSection)
    {     
        serviceCollection.Configure<RetryPolicyOptions>(retryPolicyConfigSection)
            .AddOptionsWithValidateOnStart<RetryPolicyOptions>();

        return serviceCollection;
    }
}

