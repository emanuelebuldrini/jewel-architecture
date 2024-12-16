using Microsoft.AspNetCore.Mvc;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.ApplicationServices;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Exceptions;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.UseCases;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon.Exceptions;
using JewelArchitecture.Examples.FunPokedex.Interface.Pokemon.Examples;
using JewelArchitecture.Examples.FunPokedex.Interface.Shared;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.FunPokedex.Interface.Pokemon;

[ApiController]
[Route("api/[controller]")]
public class PokemonController(PokemonService pokemonService,
    PokemonTranslatedCase pokemonTranslatedCase) : ControllerBase
{
    /// <summary>
    /// Get a Pokémon by name.
    /// </summary>
    /// <param name="name">The name of the Pokémon.</param>
    /// <response code="200">Returns the Pokémon details successfully.</response>
    /// <response code="404">If the Pokémon is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{name}")]
    [ProducesResponseType<PokemonAggregate>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status200OK,typeof(PokemonExample))]
    public async Task<ActionResult> GetPokemonAsync([MaxLength(32)][Required] string name)
    {
        try
        {
            var pokemon = await pokemonService.GetAsync(name);

            return Ok(pokemon);
        }
        catch (PokemonNotFoundException)
        {
            return NotFound();
        }
        catch (PokemonDataFetchException exception)
        {
            return ApiResponseHelper.InternalServerError(exception);
        }
    }

    /// <summary>
    /// Get a Pokémon by name with its description translated.
    /// </summary>
    /// <remarks>
    /// Catch a Pokémon with a cave habitat or legendary status to receive the Yoda translation.
    /// For all others, the Shakespeare translation will be applied.
    /// Beware, young trainer! This API is rate-limited—too many requests, and you might exhaust the patience of the server.
    /// If that happens, the standard Pokémon description will be your consolation prize.
    /// </remarks>
    /// <param name="name">The name of the Pokémon.</param>
    /// <response code="200">Returns the Pokémon details with a translated description.</response>
    /// <response code="404">If the Pokémon is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("Translated/{name}")]
    [ProducesResponseType<PokemonAggregate>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TranslatedPokemonExample))]
    public async Task<ActionResult> GetPokemonTranslatedAsync([MaxLength(32)][Required] string name)
    {
        try
        {
            var useCaseInput = new PokemonTranslatedInput(name);

            var pokemonTranslated = await pokemonTranslatedCase.HandleAsync(useCaseInput);

            return Ok(pokemonTranslated);
        }
        catch (PokemonNotFoundException)
        {
            return NotFound();
        }
        catch (PokemonDataFetchException exception)
        {
            return ApiResponseHelper.InternalServerError(exception);
        }
    }
}
