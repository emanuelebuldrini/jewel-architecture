using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;

public sealed class UpdateGroupCapacityCase(ILockService<GroupAggregate, Guid> groupLockService,
        ILockService<ChargeStationAggregate, Guid> chargeStationLockService,
        IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler,
        IAggregateCommandHandler<GroupAggregate, Guid, UpdateGroupCapacityCommand<GroupAggregate>> updateGroupCapacityCommandHandler,
        GroupCapacityValidatorService capacityValidator) : NoOutputUseCase<UpdateGroupCapacityInput>
{
    protected override async Task HandleNoOutputAsync(UpdateGroupCapacityInput input)
    {    
        using var groupLock = await groupLockService.AcquireLockAsync();
        // Need a lock also on charge station to make sure to not read connectors stale data.
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

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

        var command = new UpdateGroupCapacityCommand<GroupAggregate>(editedGroupCapacity, result.Group);
        await updateGroupCapacityCommandHandler.HandleAsync(command);
    }
}
