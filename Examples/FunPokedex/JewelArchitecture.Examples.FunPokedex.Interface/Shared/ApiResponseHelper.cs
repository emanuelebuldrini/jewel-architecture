using Microsoft.AspNetCore.Mvc;
using JewelArchitecture.Examples.FunPokedex.Application.Pokemon.Exceptions;

namespace JewelArchitecture.Examples.FunPokedex.Interface.Shared;

public static class ApiResponseHelper
{
    public static ActionResult InternalServerError(PokemonDataFetchException exception)
    {
        var suggestions = "Please ensure the server is connected to the network, and try again later.";

        return new ObjectResult(new { message = $"{exception.Message} {suggestions}" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };      
    }
}
