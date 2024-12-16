using JewelArchitecture.Examples.FunPokedex.Application.Shared;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;

public class PokemonSpeciesDto : NameDto
{
    public required NameDto Habitat { get; set; }
    public required bool IsLegendary { get; set; }
    public required FlavorTextEntryDto[] FlavorTextEntries { get; set; }
}