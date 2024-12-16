
using JewelArchitecture.Core.Application.QueryHandlers;
using Microsoft.Extensions.Logging;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Exceptions;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Queries;
using JewelArchitecture.Examples.FunPokedex.Application.Shared;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.ApplicationServices;

public class PokemonService(IQueryHandler<PokemonByNameQuery, PokemonDto> pokemonByNameQueryHandler,
     IQueryHandler<PokemonSpeciesByNameQuery, PokemonSpeciesDto> pokemonSpeciesByNameQueryHandler,
     ILogger<PokemonService> logger)
{
    public async Task<PokemonAggregate> GetAsync(string name)
    {
        PokemonDto pokemon;
        PokemonSpeciesDto pokemonSpecies;
        try
        {
            // Get the Pokemon first
            pokemon = await pokemonByNameQueryHandler.HandleAsync(new PokemonByNameQuery(name));
            // Then retrieve the Pokemon species details
            var speciesQuery = new PokemonSpeciesByNameQuery(pokemon.Species.Name);
            pokemonSpecies = await pokemonSpeciesByNameQueryHandler.HandleAsync(speciesQuery);
        }
        catch (HttpRequestException exception)
        {
            var appException = new PokemonDataFetchException(name);
            logger.LogError(exception, appException.Message);

            throw appException;
        }

        // Pokemon flavor text comes in different languages and depends on the version of the game.
        var flavorText = pokemonSpecies.FlavorTextEntries
            // By default take the first version of the game in English language, e.g. Red or Blue.
            .FirstOrDefault(e => e.Language.Name == "en")?.FlavorText;

        // Flavor text is returned with common control characters like \n, \f.
        // And it seems that the word 'Pokémon' has a wrong casing: POKéMON.
        var sanitizedFlavorText = Utils.SanitizeFlavorText(flavorText);

        return new PokemonAggregate
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Description = sanitizedFlavorText,
            Habitat = pokemonSpecies.Habitat.Name,
            IsLegendary = pokemonSpecies.IsLegendary
        };
    }
}