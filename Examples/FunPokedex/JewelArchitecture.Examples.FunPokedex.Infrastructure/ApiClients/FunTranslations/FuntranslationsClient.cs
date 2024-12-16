using Microsoft.Extensions.Options;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Application.Shared.FunTranslations;
using JewelArchitecture.Examples.FunPokedex.Infrastructure.Caching;
using System.Text.Json;

namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients.FunTranslations;

public sealed class FuntranslationsClient(HttpClient httpClient, IOptions<FuntranslationsApiOptions> options,
    IStreamCachingService cachingService)
    : ApiClient(httpClient, options, cachingService), IFuntranslationsClient
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        // Funtranslations API uses a lowercase convention by default.
        PropertyNameCaseInsensitive = true
    };

    protected override string ApiName { get => "Funtranslations"; }

    protected override JsonSerializerOptions JsonOptions { get => _jsonOptions; }
}