namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Exceptions;

public class PokemonDataFetchException(string pokemonName) 
    :Exception($"Unable to fetch Pokemon data for '{pokemonName}'.");