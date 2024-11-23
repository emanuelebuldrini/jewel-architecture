using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class RemoveConnectorCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<RemoveConnectorCommand,ChargeStationAggregate>
{
    public async Task HandleAsync(RemoveConnectorCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.RemoveConnector(cmd.ConnectorId);        

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}