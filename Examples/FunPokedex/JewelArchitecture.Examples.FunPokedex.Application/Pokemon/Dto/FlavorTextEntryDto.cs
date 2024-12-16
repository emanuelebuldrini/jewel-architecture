using JewelArchitecture.Examples.FunPokedex.Application.Shared;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;

public class FlavorTextEntryDto
{
    public required string FlavorText { get; set; }
    public required NameDto Language { get; set; }
    public required NameDto Version { get; set; }
}