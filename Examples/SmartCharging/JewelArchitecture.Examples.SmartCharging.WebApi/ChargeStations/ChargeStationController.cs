using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.WebApi.ChargeStations.DtoExamples;
using JewelArchitecture.Examples.SmartCharging.WebApi.Shared;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.ChargeStations;

[ApiController]
[Route("api/[controller]")]
public class ChargeStationController(ChargeStationService chargeStationService,
    GroupService groupService,
    CreateChargeStationCase createChargeStationCase,
    RemoveChargeStationCase removeChargeStationCase,
    ChangeChargeStationGroupCase changeChargeStationGroupCase) : ControllerBase
{
    /// <summary>
    /// Create a new Charge Station with at least 1 connector and up to 5 Connectors.
    /// </summary>
    /// <remarks>
    /// It is required a reference to a Group to create the Charge Station.
    /// Connectors must have a unique ID within the range of 1 to 5.
    /// Each connector requires to specify a max current of minimum 1 ampere.
    /// The sum of the max current of each Connector related to the Group must be less or equal than the Group capacity.
    /// </remarks>
    /// <param name="createDto">
    /// The DTO to create the Charge Station: it includes the name, an array of Connectors and the Group reference.
    /// </param>
    /// <response code="201">The ID of the Charge Station that was created.</response>
    /// <response code="400">Business validation failed due to a violation of a domain invariant or invalid input values.</response>
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(ChargeStationCreateDtoExample), typeof(ChargeStationCreateDto))]
    public async Task<IActionResult> PostAsync([FromBody] ChargeStationCreateDto createDto)
    {
        var groupId = createDto.GroupReference!.Value;
        if (!await groupService.ExistsAsync(groupId))
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(nameof(createDto.GroupReference),
               $"Group reference with ID '{groupId}' not found.");

            return BadRequest(validationDetails);
        }

        try
        {
            var useCaseInput = DtoUseCaseInputMapper.MapCreateChargeStationDto(createDto);
            var chargeStationId = await createChargeStationCase.HandleAsync(useCaseInput);

            return CreatedAtAction(nameof(GetAsync), new { id = chargeStationId }, chargeStationId);
        }
        catch (ExceedsGroupCapacityException exception)
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(exception.GetType().Name,
                exception.Message);

            return BadRequest(validationDetails);
        }
    }

    /// <summary>
    /// Get a Charge Station by ID.
    /// </summary>
    /// <remarks>
    /// Get Charge Station properties and related Connectors.
    /// </remarks>
    /// <param name="id">The ID of the Charge Station.</param>
    /// <response code="200">The Charge Station was successfully returned in the response.</response>
    /// <response code="404">Charge Station not found.</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<ChargeStationAggregate>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        if (!await chargeStationService.ExistsAsync(id))
        {
            return NotFound();
        }

        var group = await chargeStationService.GetSingleAsync(id);

        return Ok(group);
    }

    /// <summary>
    /// Change the Group of a Charge Station.
    /// </summary>
    /// <param name="id">The ID of the Charge Station.</param>
    /// <param name="dto">The DTO to change the Group: it includes the reference to the new group.</param>
    /// <response code="204">The Charge Station group was changed successfully.</response>
    /// <response code="400">Business validation failed due to a violation of a domain invariant or invalid input values.</response>
    /// <response code="404">Charge Station not found.</response>
    [Route("{id:guid}/Group")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutGroupAsync(Guid id, [FromBody] ChargeStationChangeGroupDto dto)
    {
        if (!await chargeStationService.ExistsAsync(id))
        {
            return NotFound();
        }

        // Check first if the group to assign the charge station exists.
        var groupId = dto.GroupId!.Value;
        if (!await groupService.ExistsAsync(groupId))
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(nameof(dto.GroupId),
             $"Group with ID '{groupId}' not found.");

            return BadRequest(validationDetails);
        }

        try
        {
            await changeChargeStationGroupCase.HandleAsync(new ChangeChargeStationGroupInput(id, dto));
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
    /// Edit properties of a Charge Station.
    /// </summary>
    /// <remarks>
    /// Update the name of a Charge Station.
    /// The maximum length of the name must be 100 characters.
    /// </remarks>
    /// <param name="id">The ID of the Charge Station.</param>
    /// <param name="editDto">The DTO to edit the Charge Station: it includes the new name.</param>
    /// <response code="204">The Charge Station properties were updated successfully.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Charge Station not found.</response>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerRequestExample(typeof(ChargeStationEditDtoExample), typeof(ChargeStationEditDto))]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] ChargeStationEditDto editDto)
    {
        if (!await chargeStationService.ExistsAsync(id))
        {
            return NotFound();
        }

        await chargeStationService.EditAsync(id, editDto);

        return NoContent();
    }

    /// <summary>
    /// Remove a Charge Station and related Connectors.
    /// </summary>
    /// <remarks>
    /// With the deletion of the Charge Station, all the associated Connectors are removed.
    /// </remarks>
    /// <param name="id">The ID of the Charge Station.</param>
    /// <response code="204">The Charge Station was successfully deleted.</response>
    /// <response code="404">Charge Station not found.</response>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!await chargeStationService.ExistsAsync(id))
        {
            return NotFound();
        }

        var useCaseInput = new RemoveChargeStationInput(id);
        await removeChargeStationCase.HandleAsync(useCaseInput);

        return NoContent();
    }
}
