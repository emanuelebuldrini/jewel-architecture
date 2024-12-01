using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public class CreateConnectorCase(ILockService<GroupAggregate, Guid> groupLockService,
    ILockService<ChargeStationAggregate, Guid> chargeStationLockService,
    IQueryHandler<GroupChargeStationConnectorQuery, GroupChargeStationConnectorResult> groupChargeStationConnectorQueryHandler,
    IAggregateCommandHandler<ChargeStationAggregate, Guid, AddConnectorCommand> addConnectorCommandHandler,
    GroupCapacityValidatorService groupCapacityValidator) : NoOutputUseCase<CreateConnectorInput>
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
