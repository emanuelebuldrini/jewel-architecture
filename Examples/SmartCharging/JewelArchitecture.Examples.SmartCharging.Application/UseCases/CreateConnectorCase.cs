using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases;

public class CreateConnectorCase(ILockService<GroupAggregate> groupLockService,
    ILockService<ChargeStationAggregate> chargeStationLockService,
    IQueryHandler<GroupChargeStationConnectorQuery, GroupChargeStationConnectorResult> groupChargeStationConnectorQueryHandler,
    IAggregateCommandHandler<AddConnectorCommand, ChargeStationAggregate> addConnectorCommandHandler,
    GroupCapacityValidatorService groupCapacityValidator): NoOutputUseCase<CreateConnectorInput>
{
    protected override async Task HandleNoOutputAsync(CreateConnectorInput input)
    {
        using var groupLock = await groupLockService.AcquireLockAsync();
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        // Retrieve all group connectors to validate the group capacity.
        var result = await groupChargeStationConnectorQueryHandler.HandleAsync(new GroupChargeStationConnectorQuery(input.ChargeStationId));

        // Prepare the connector to create.
        var connectorId = new ConnectorId(input.Dto.Id!.Value);
        var connectorMaxCurrent = new AmpereUnit(input.Dto.MaxCurrentAmps!.Value);

        // Let the domain service decide if the charge station connector can be created.
        var (canAddConnector, validationError) = groupCapacityValidator.CanAddChargeStationConnector(connectorMaxCurrent,
            result.GroupConnectors.Group.Capacity, result.GroupConnectors.Connectors);

        if (!canAddConnector)
        {
            throw new ExceedsGroupCapacityException(validationError);
        }

        var command = new AddConnectorCommand(connectorId, connectorMaxCurrent, result.ChargeStation);
        await addConnectorCommandHandler.HandleAsync(command);
    }
}
