using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

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