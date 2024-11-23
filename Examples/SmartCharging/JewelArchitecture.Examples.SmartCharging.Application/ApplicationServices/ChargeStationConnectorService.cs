using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.Entities;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;

public class ChargeStationConnectorService(ILockService<ChargeStationAggregate> chargeStationLockService,
        IQueryHandler<ChargeStationByIdQuery,ChargeStationAggregate> chargeStationByIdQueryHandler,
        IAggregateExistsQueryHandler<ChargeStationAggregate, AggregateExistsQuery<ChargeStationAggregate>> chargeStationExistsQueryHandler,
        IAggregateCommandHandler<RemoveConnectorCommand, ChargeStationAggregate> removeConnectorCommandHandler
)
{
    public async Task<ChargeStationConnectorEntity> GetSingleAsync(Guid chargeStationId, ConnectorId connectorId)
    {
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(new ChargeStationByIdQuery(chargeStationId));
        return chargeStation.Connectors.Single(c => c.Id == connectorId);
    }       

    public async Task RemoveAsync(Guid chargeStationId, ConnectorId connectorId)
    {
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        var chargeStation = await GetChargeStation(chargeStationId);
        var command = new RemoveConnectorCommand(chargeStation, connectorId);

        await removeConnectorCommandHandler.HandleAsync(command);
    }   

    public async Task<bool> ExistsAsync(Guid chargeStationId, ConnectorId id)
    {
        var query = new AggregateExistsQuery<ChargeStationAggregate>(chargeStationId);
        if (!await chargeStationExistsQueryHandler.HandleAsync(query))
        {
            return false;
        }

        var chargeStation = await GetChargeStation(chargeStationId);
        return chargeStation.Connectors.Any(c => c.Id == id);
    }

    private async Task<ChargeStationAggregate> GetChargeStation(Guid chargeStationId)
    {
        return await chargeStationByIdQueryHandler.HandleAsync(new ChargeStationByIdQuery(chargeStationId));
    }
}
