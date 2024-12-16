using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Queries;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon.Exceptions;
using System.Net;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.QueryHandlers;

public class PokemonByNameQueryHandler(IPokeapiClient pokeapiClient)
    : IQueryHandler<PokemonByNameQuery, PokemonDto>
{
    public async Task<PokemonDto> HandleAsync(PokemonByNameQuery query)
    {
        // Pokeapi is case-sensitive and lowercase by default.
        // Make it here case-insensitive to be more user-friendly.
        var pokemonName = query.Name.ToLowerInvariant();
        var relativeUri = $"pokemon/{pokemonName}";

        try
        {
            return await pokeapiClient.FetchAsync<PokemonDto>(relativeUri, cacheKey: $"{pokemonName}.pokemon");
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound)
            {
                throw new PokemonNotFoundException(query.Name);
            }
            throw;
        }
    }
}
