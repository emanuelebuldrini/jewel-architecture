using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.FunPokedex.Application.Abstractions;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Queries;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.QueryHandlers;

public class PokemonSpeciesByNameQueryHandler(IPokeapiClient pokeapiClient)
    : IQueryHandler<PokemonSpeciesByNameQuery, PokemonSpeciesDto>
{
    public async Task<PokemonSpeciesDto> HandleAsync(PokemonSpeciesByNameQuery query)
    {
        // Details about the Pokemon species is located in a dedicated endpoint.
        var speciesName = query.Name;
        var relativeUri = $"pokemon-species/{speciesName}";
        return await pokeapiClient.FetchAsync<PokemonSpeciesDto>(relativeUri, cacheKey: $"{speciesName}.species");
    }
}
