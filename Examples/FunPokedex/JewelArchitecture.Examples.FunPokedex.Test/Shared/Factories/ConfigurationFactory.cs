using Microsoft.Extensions.Configuration;

namespace JewelArchitecture.Examples.FunPokedex.Test.Shared.Factories;

internal class ConfigurationFactory
{
    public static IConfigurationRoot GetExternalApiConfig()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Pokeapi:BaseUrl", "https://pokeapi.co/api/v2" },
            { "Pokeapi:CacheDuration", "10675199.02:48:05" },
            { "FuntranslationsApi:BaseUrl", "https://api.funtranslations.com/translate" },
            // Set essentially no cache expiration for testing purposes: duration of TimeSpan.MaxValue.
            { "FuntranslationsApi:CacheDuration", "10675199.02:48:05" },
            // Use a simple retry policy for testing purposes: short delay.
            { "RetryPolicy:BackoffStrategy", "Constant" },
            { "RetryPolicy:RetryCount", "3" },
            { "RetryPolicy:BaseDelaySeconds", "0.1" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }
}
