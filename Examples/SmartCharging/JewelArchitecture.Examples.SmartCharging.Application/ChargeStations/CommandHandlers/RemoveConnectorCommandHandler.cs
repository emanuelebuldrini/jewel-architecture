using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class RemoveConnectorCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<RemoveConnectorCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(RemoveConnectorCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.RemoveConnector(cmd.ConnectorId);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}