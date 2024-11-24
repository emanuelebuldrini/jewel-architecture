using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Groups.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.WebApi.Groups.DtoExamples;
using JewelArchitecture.Examples.SmartCharging.WebApi.Shared;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Groups;

[ApiController]
[Route("api/[controller]")]
public class GroupController(GroupService groupService,
    UpdateGroupCapacityCase updateGroupCapacityCase,
    RemoveGroupCase removeGroupCase) : ControllerBase
{
    /// <summary>
    /// Create a new Group.
    /// </summary>
    /// <remarks>
    /// Create a Group with a specified capacity in amperes.
    /// </remarks>
    /// <param name="createDto"></param>
    /// <response code="201">The ID of the Group that was created.</response>
    /// <response code="400">Business validation failed due to a violation of a domain invariant or invalid input values.</response>
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(GroupCreateDtoExample), typeof(GroupCreateDto))]
    public async Task<IActionResult> PostAsync([FromBody] GroupCreateDto createDto)
    {
        var groupId = await groupService.CreateAsync(createDto);

        return CreatedAtAction(nameof(GetAsync), new { id = groupId }, groupId);
    }

    /// <summary>
    /// Get a Group by ID.
    /// </summary>
    /// <remarks>
    /// Get Group properties and related Charge Stations.
    /// </remarks>
    /// <param name="id">The ID of the Group.</param>
    /// <response code="200">The Group was successfully returned in the response.</response>
    /// <response code="404">Group not found.</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<GroupAggregate>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        if (!await groupService.ExistsAsync(id))
        {
            return NotFound();
        }

        var group = await groupService.GetSingleAsync(id);

        return Ok(group);
    }

    /// <summary>
    /// Update the Group capacity.
    /// </summary>
    /// <remarks>
    /// The sum of the max current of each Connector related to the Group must be less or equal than the Group capacity.
    /// </remarks>
    /// <param name="id">The ID of the Group.</param>
    /// <param name="editDto">The DTO to update the capacity of the Group: it includes the new capacity in amperes.</param>
    /// <response code="204">The Group capacity was successfully updated.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Charge Station or Connector not found.</response>
    [HttpPut]
    [Route("{id:guid}/Capacity")]
    [ProducesResponseType<GroupAggregate>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCapacityAsync(Guid id, [FromBody] GroupUpdateCapacityDto editDto)
    {
        if (!await groupService.ExistsAsync(id))
        {
            return NotFound();
        }

        try
        {
            await updateGroupCapacityCase.HandleAsync(new UpdateGroupCapacityInput(id, editDto));
        }
        catch (InvalidGroupCapacityException exception)
        {
            var validationDetails = BusinessValidationHelper.GetValidationProblemDetails(exception.GetType().Name,
                exception.Message);

            return BadRequest(validationDetails);
        }

        return NoContent();
    }

    /// <summary>
    /// Edit properties of a Group.
    /// </summary>
    /// <remarks>
    /// Update the name of a Group.
    /// The maximum length of the name must be 100 characters.
    /// </remarks>
    /// <param name="id">The ID of the Group.</param>
    /// <param name="editDto">The DTO to edit the Group: it includes the new name.</param>
    /// <response code="204">The Group properties were updated successfully.</response>
    /// <response code="400">Validation failed due to invalid input values.</response>
    /// <response code="404">Group not found.</response>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<GroupAggregate>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] GroupEditDto editDto)
    {
        if (!await groupService.ExistsAsync(id))
        {
            return NotFound();
        }

        await groupService.EditAsync(id, editDto);

        return NoContent();
    }

    /// <summary>
    /// Remove a Group and related Charge Stations.
    /// </summary>
    /// <remarks>
    /// With the deletion of a Group, all the associated Charge Stations are removed.
    /// </remarks>
    /// <param name="id">The ID of the Group.</param>
    /// <response code="204">The group was successfully deleted.</response>
    /// <response code="404">Group not found.</response>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!await groupService.ExistsAsync(id))
        {
            return NotFound();
        }

        var useCaseInput = new RemoveGroupInput(id);
        await removeGroupCase.HandleAsync(useCaseInput);

        return NoContent();
    }
}
