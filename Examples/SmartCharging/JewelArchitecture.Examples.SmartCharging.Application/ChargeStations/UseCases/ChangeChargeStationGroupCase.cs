using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared.DomainServices;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public sealed class ChangeChargeStationGroupCase(ILockService<ChargeStationAggregate> chargeStationLockService,
        ILockService<GroupAggregate> groupLockService,
        IQueryHandler<ChangeGroupChargeStationConnectorQuery, GroupChargeStationConnectorResult> groupChargeStationConnectorQueryHandler,
        IAggregateCommandHandler<ChangeGroupCommand, ChargeStationAggregate> changeGroupCommandHandler,
        GroupCapacityValidatorService groupCapacityValidator) : NoOutputUseCase<ChangeChargeStationGroupInput>
{
    protected override async Task HandleNoOutputAsync(ChangeChargeStationGroupInput input)
    {
        var groupId = input.Dto.GroupId!.Value;

        using var groupLock = await groupLockService.AcquireLockAsync();
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        // Retrieve all group connectors of the new group to validate the group capacity.
        var query = new ChangeGroupChargeStationConnectorQuery(input.ChargeStationId, groupId);
        var result = await groupChargeStationConnectorQueryHandler.HandleAsync(query);

        // Let the domain service decide if the charge station can be associated with the new group.
        var (canAddConnectors, validationError) = groupCapacityValidator.CanAddChargeStationConnectors(result.ChargeStation.Connectors,
            result.GroupConnectors.Group.Capacity,
            result.GroupConnectors.Connectors);

        if (!canAddConnectors)
        {
            throw new ExceedsGroupCapacityException(validationError);
        }

        var command = new ChangeGroupCommand(groupId, result.ChargeStation);
        await changeGroupCommandHandler.HandleAsync(command);
    }
}
