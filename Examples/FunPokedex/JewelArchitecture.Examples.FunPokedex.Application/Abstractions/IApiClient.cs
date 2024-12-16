namespace JewelArchitecture.Examples.FunPokedex.Application.Abstractions;

public interface IApiClient
{
    Task<TDeserialize> FetchAsync<TDeserialize>(string relativeUri, string? cacheKey)
        where TDeserialize : class;
}
