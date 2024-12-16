using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Queries;

public record PokemonSpeciesByNameQuery(string Name) : IQuery;