using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.Exceptions;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.Pokeapi;

namespace JewelArchitecture.Examples.FunPokedex.Test.Shared.Fakes;

internal sealed class NastyPokeapiClient(PokeapiClient client) : IPokeapiClient
{
    int retryCount;

    public async Task<TDeserialize> FetchAsync<TDeserialize>(string relativeUri, string? cacheKey)
        where TDeserialize : class
    {
        while (retryCount < 3)
        {
            // It should try at least 3 times.
            retryCount++;
            throw new HttpRetryableException(new HttpRequestException(HttpRequestError.Unknown,
                "Sorry, unable to reach the Pokeapi.", null, System.Net.HttpStatusCode.Ambiguous));
        }

        retryCount = 0;

        return await client.FetchAsync<TDeserialize>(relativeUri, cacheKey);
    }
}