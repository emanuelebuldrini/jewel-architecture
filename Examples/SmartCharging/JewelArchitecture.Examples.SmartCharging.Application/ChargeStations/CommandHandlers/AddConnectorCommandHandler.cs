using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

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