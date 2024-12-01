using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;

public class ChargeStationConnectorService(ILockService<ChargeStationAggregate, Guid> chargeStationLockService,
        IAggregateByIdQueryHandler<ChargeStationAggregate, Guid, AggregateByIdQuery<ChargeStationAggregate, Guid>> chargeStationByIdQueryHandler,
        IAggregateExistsQueryHandler<ChargeStationAggregate, Guid, AggregateExistsQuery<ChargeStationAggregate, Guid>> chargeStationExistsQueryHandler,
        IAggregateCommandHandler<ChargeStationAggregate, Guid, RemoveConnectorCommand<ChargeStationAggregate>> removeConnectorCommandHandler
)
{
    public async Task<ChargeStationConnectorEntity> GetSingleAsync(Guid chargeStationId, ConnectorId connectorId)
    {
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(new AggregateByIdQuery<ChargeStationAggregate, Guid>(chargeStationId));
        return chargeStation.Connectors.Single(c => c.Id == connectorId);
    }

    public async Task RemoveAsync(Guid chargeStationId, ConnectorId connectorId)
    {
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        var chargeStation = await GetChargeStation(chargeStationId);
        var command = new RemoveConnectorCommand<ChargeStationAggregate>(chargeStation, connectorId);

        await removeConnectorCommandHandler.HandleAsync(command);
    }

    public async Task<bool> ExistsAsync(Guid chargeStationId, ConnectorId id)
    {
        var query = new AggregateExistsQuery<ChargeStationAggregate, Guid>(chargeStationId);
        if (!await chargeStationExistsQueryHandler.HandleAsync(query))
        {
            return false;
        }

        var chargeStation = await GetChargeStation(chargeStationId);
        return chargeStation.Connectors.Any(c => c.Id == id);
    }

    private async Task<ChargeStationAggregate> GetChargeStation(Guid chargeStationId)
    {
        return await chargeStationByIdQueryHandler.HandleAsync(new AggregateByIdQuery<ChargeStationAggregate, Guid>(chargeStationId));
    }
}
