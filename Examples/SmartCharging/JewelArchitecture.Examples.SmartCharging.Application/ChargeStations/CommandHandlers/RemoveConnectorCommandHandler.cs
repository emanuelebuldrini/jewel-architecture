using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class RemoveConnectorCommandHandler(IRepository<ChargeStationAggregate, Guid> chargeStationRepo)
    : IAggregateCommandHandler<ChargeStationAggregate, Guid, RemoveConnectorCommand<ChargeStationAggregate>>
{
    public async Task HandleAsync(RemoveConnectorCommand<ChargeStationAggregate> cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.RemoveConnector(cmd.ConnectorId);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}