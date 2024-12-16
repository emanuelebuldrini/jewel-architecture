using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.FunPokedex.Interface.Pokemon.Examples;

public class TranslatedPokemonExample : IExamplesProvider<PokemonAggregate>
{
    public PokemonAggregate GetExamples() =>
        new PokemonAggregate
        {
            Id = 151,
            Name = "mew",
            Description = "So rare yond 't is still did doth sayeth to beest a mirage by many experts. Only a few people hath't seen 't worldwide.",
            Habitat = "rare",
            IsLegendary = false
        };
}
