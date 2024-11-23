using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Core.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases;

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
