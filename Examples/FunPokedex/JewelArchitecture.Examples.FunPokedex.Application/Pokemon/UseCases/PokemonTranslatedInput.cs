using JewelArchitecture.Core.Application.UseCases;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.UseCases
{
    public record PokemonTranslatedInput(string PokemonName) : IUseCaseInput;
}