using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Application.Shared.FunTranslations.Dto;

namespace JewelArchitecture.Examples.FunPokedex.Application.Shared.FunTranslations;

public class FunTranslationQueryByTextHandler(IFuntranslationsClient funtranslationsClient)
    : IQueryHandler<FunTranslationQueryByText, string>
{
    // Use JSON format to get responses from Funtranslations: API default is XML.
    private readonly string _apiResponseFormat = "json";

    public async Task<string> HandleAsync(FunTranslationQueryByText query)
    {
        var endpoint = query.TranslationType.ToString().ToLowerInvariant();
        var queryString = $"text={query.Text}";
        var relativeUri = $"{endpoint}.{_apiResponseFormat}?{queryString}";
        var response = await funtranslationsClient.FetchAsync<FunTranslationsResponse>(relativeUri, 
            query.CacheKey);

        return response.Contents.Translated;
    }
}
