using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public sealed class UpdateConnectorMaxCurrentCase(ILockService<GroupAggregate> groupLockService,
    ILockService<ChargeStationAggregate> chargeStationLockService,
    IQueryHandler<GroupChargeStationConnectorQuery, GroupChargeStationConnectorResult> groupChargeStationConnectorQueryHandler,
    IAggregateCommandHandler<UpdateMaxCurrentCommand, ChargeStationAggregate> updateMaxCurrentCommandHandler,
    GroupCapacityValidatorService groupCapacityValidator) : NoOutputUseCase<UpdateConnectorMaxCurrentInput>
{
    protected override async Task HandleNoOutputAsync(UpdateConnectorMaxCurrentInput input)
    {
        using var groupLock = await groupLockService.AcquireLockAsync();
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        // Retrieve all group connectors to validate the group capacity.
        var result = await groupChargeStationConnectorQueryHandler.HandleAsync(new GroupChargeStationConnectorQuery(input.ChargeStationId));

        // Get the connector to update.
        var connector = result.ChargeStation.Connectors.Single(s => s.Id == input.ConnectorId);

        // Let the domain service decide if the charge station connector max current can be updated.
        var newMaxCurrent = new AmpereUnit(input.Dto.MaxCurrentAmps!.Value);
        var (canUpdate, validationResult) = groupCapacityValidator.CanUpdateChargeStationConnectorMaxCurrent(
           newMaxCurrent, result.GroupConnectors.Group.Capacity, connectorToUpdate: connector,
            result.GroupConnectors.Connectors);

        if (!canUpdate)
        {
            throw new ExceedsGroupCapacityException(validationResult);
        }

        var command = new UpdateMaxCurrentCommand(connector.Id, newMaxCurrent, result.ChargeStation);
        await updateMaxCurrentCommandHandler.HandleAsync(command);
    }
}
