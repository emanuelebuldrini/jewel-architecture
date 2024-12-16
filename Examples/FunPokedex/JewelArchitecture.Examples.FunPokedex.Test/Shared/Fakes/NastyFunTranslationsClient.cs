using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.Exceptions;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.FunTranslations;

namespace JewelArchitecture.Examples.FunPokedex.Test.Shared.Fakes;

internal sealed class NastyFuntranslationsClient(FuntranslationsClient client) : IFuntranslationsClient
{
    int retryCount;

    public async Task<TDeserialize> FetchAsync<TDeserialize>(string relativeUri, string? cacheKey)
        where TDeserialize : class
    {
        while (retryCount < 2)
        {
            // It should try at least 2 times.
            retryCount++;
            throw new HttpRetryableException(new HttpRequestException());
        }

        retryCount = 0;

        return await client.FetchAsync<TDeserialize>(relativeUri, cacheKey);
    }
}