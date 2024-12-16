
using JewelArchitecture.Examples.FunPokedex.Application.Shared;

namespace JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Dto;

public class PokemonDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required NameDto Species { get; set; }
}
