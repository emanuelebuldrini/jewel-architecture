using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Queries;

public record PokemonByNameQuery(string Name): IQuery;