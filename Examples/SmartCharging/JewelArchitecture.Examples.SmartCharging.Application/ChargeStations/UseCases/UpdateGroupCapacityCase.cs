using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Core.Application.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public sealed class UpdateGroupCapacityCase(ILockService<GroupAggregate> groupLockService,
        IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler,
        IAggregateCommandHandler<UpdateGroupCapacityCommand, GroupAggregate> updateGroupCapacityCommandHandler,
        GroupCapacityValidatorService capacityValidator) : NoOutputUseCase<UpdateGroupCapacityInput>
{
    protected override async Task HandleNoOutputAsync(UpdateGroupCapacityInput input)
    {
        using var groupLock = await groupLockService.AcquireLockAsync();

        // Retrieve all group connectors to validate the group capacity.
        var result = await groupConnectorQueryHandler.HandleAsync(new GroupConnectorQuery(input.GroupId));

        // Let the domain service decide if the group capacity can be updated.
        var editedGroupCapacity = new AmpereUnit(input.Dto.CapacityAmps!.Value);
        var (canUpdate, validationError) = capacityValidator.CanUpdateGroupCapacity(editedGroupCapacity,
            result.Connectors);

        if (!canUpdate)
        {
            throw new InvalidGroupCapacityException(validationError);
        }

        var command = new UpdateGroupCapacityCommand(editedGroupCapacity, result.Group);
        await updateGroupCapacityCommandHandler.HandleAsync(command);
    }
}
