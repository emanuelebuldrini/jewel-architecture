using JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Controllers;

[ApiController]
[Route("api/ChargeStation/{chargeStationId:guid}/[controller]")]
public class ChargeStationConnectorController(ChargeStationService chargeStationService,
    ChargeStationConnectorService chargeStationConnectorService,
    UpdateConnectorMaxCurrentCase updateConnectorMaxCurrentCase,
    CreateConnectorCase createConnectorCase) : ControllerBase
{
    /// <summary>
    /// Create a new Connector associated to a Charge Station.
    /// </summary>
    /// <param name="chargeStationId">The ID of the Charge Station.</param>
    /// <param name="newConnector">The DTO to create the Connector: it includes the ID and the maximum capacity in amperes.</param>
    /// <response code="201">The ID of the Connector that was created.</response>
    /// <response code="400">Business validation failed due to a violation of a domain invariant or invalid input values.</response>
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(Guid chargeStationId, [FromBody] ChargeStationConnectorCreateDto newConnector)
    {
        if (!await chargeStationService.ExistsAsync(chargeStationId))
        {
            return NotFound();
        }

        try
        {
            await createConnectorCase.HandleAsync(new CreateConnectorInput(chargeStationId, newConnector));
        }
        catch (Exception exception) when (exception is ExceedsGroupCapacityException ||
                                            exception is ConnectorIdNotUniqueException)
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(exception.GetType().Name,
                exception.Message);

            return BadRequest(validationDetails);
        }

        var connectorId = newConnector.Id!.Value;
        return CreatedAtAction(nameof(GetAsync), new { chargeStationId, id = connectorId }, connectorId);
    }

    /// <summary>
    /// Get a Connector by ID in the context of a Charge Station.
    /// </summary>
    /// <remarks>
    /// Get Connector details related to a Charge Station.
    /// </remarks>
    /// <param name="chargeStationId">The ID of the Charge Station to which the Connector belongs.</param>
    /// <param name="id">The ID of a Connector in the context of the Charge Station.</param>
    /// <response code="200">The Connector related to the Charge Station was successfully returned in the response.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Charge Station or Connector not found.</response>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType<ChargeStationConnectorEntity>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid chargeStationId, [Range(1, 5)] int id)
    {
        var connectorId = new ConnectorId(id);

        if (!await chargeStationConnectorService.ExistsAsync(chargeStationId, connectorId))
        {
            return NotFound();
        }

        var connector = await chargeStationConnectorService.GetSingleAsync(chargeStationId, connectorId);

        return Ok(connector);
    }

    /// <summary>
    /// Update the max current of a Connector.
    /// </summary>
    /// <remarks>
    /// The sum of the max current of each Connector related to the Group must be less or equal than the Group capacity.
    /// </remarks>
    /// <param name="chargeStationId">The ID of the Charge Station to which the Connector belongs.</param>
    /// <param name="id">The ID of the Connector in the context of a Charge Station.</param>
    /// <param name="dto">The DTO to update the Connector max current property.</param>
    /// <response code="201">The max current property of the Connector was successfully updated.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Charge Station or Connector not found.</response>
    [HttpPut]
    [Route("{id:int}/MaxCurrent")]
    [ProducesResponseType<ChargeStationAggregate>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutMaxCurrentAsync(Guid chargeStationId, [Range(1, 5)] int id,
        [FromBody] ChargeStationConnectorUpdateMaxCurrentDto dto)
    {
        var connectorId = new ConnectorId(id);

        if (!await chargeStationConnectorService.ExistsAsync(chargeStationId, connectorId))
        {
            return NotFound();
        }

        try
        {
            await updateConnectorMaxCurrentCase.HandleAsync(new UpdateConnectorMaxCurrentInput(chargeStationId,
                connectorId, dto));
        }
        catch (ExceedsGroupCapacityException exception)
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(exception.GetType().Name,
                exception.Message);

            return BadRequest(validationDetails);
        }

        return NoContent();
    }

    /// <summary>
    /// Remove a Connector.
    /// </summary>
    /// <remarks>
    /// A connector is deleted and is not part anymore of a Charge Station.
    /// </remarks>
    /// <param name="chargeStationId">The ID of the Charge Station to which the Connector belongs.</param>
    /// <param name="id">The ID of the Connector in the context of the Charge Station.</param>
    /// <response code="204">The Connector was successfully deleted.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Charge Station or Connector not found.</response>
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid chargeStationId, [Range(1, 5)] int id)
    {
        var connectorId = new ConnectorId(id);
        if (!await chargeStationConnectorService.ExistsAsync(chargeStationId, connectorId))
        {
            return NotFound();
        }

        try
        {
            await chargeStationConnectorService.RemoveAsync(chargeStationId, connectorId);
        }
        catch (OneConnectorRequiredException exception)
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(exception.GetType().Name,
               exception.Message);

            return BadRequest(validationDetails);
        }

        return NoContent();
    }
}
