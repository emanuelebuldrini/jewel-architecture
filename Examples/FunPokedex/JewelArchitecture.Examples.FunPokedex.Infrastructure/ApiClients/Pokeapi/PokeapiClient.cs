using Microsoft.Extensions.Options;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.Caching;
using System.Text.Json;

namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.Pokeapi;

public sealed class PokeapiClient(HttpClient httpClient, IOptions<PokeapiOptions> options,
    IStreamCachingService cachingService)
    : ApiClient(httpClient, options, cachingService), IPokeapiClient
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        // Snake case is the default style used by Pokeapi.
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    protected override string ApiName => "Pokeapi";
    protected override JsonSerializerOptions JsonOptions { get => _jsonOptions; }
}