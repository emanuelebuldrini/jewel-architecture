using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class AddConnectorCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<AddConnectorCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(AddConnectorCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.AddConnector(cmd.ConnectorId, cmd.ConnectorMaxCurrent);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}