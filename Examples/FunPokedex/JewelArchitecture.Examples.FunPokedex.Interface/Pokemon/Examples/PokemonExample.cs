using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.FunPokedex.Interface.Pokemon.Examples;

public class PokemonExample : IExamplesProvider<PokemonAggregate>
{
    public PokemonAggregate GetExamples() =>
        new PokemonAggregate
        {
            Id = 25,
            Name = "pikachu",
            Description = "When several of these Pokémon gather, their electricity could build and cause lightning storms.",
            Habitat = "forest",
            IsLegendary = false
        };
}
