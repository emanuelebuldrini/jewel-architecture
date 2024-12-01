using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class AddConnectorCommandHandler(IRepository<ChargeStationAggregate, Guid> chargeStationRepo)
    : IAggregateCommandHandler<ChargeStationAggregate, Guid, AddConnectorCommand>
{
    public async Task HandleAsync(AddConnectorCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.AddConnector(cmd.ConnectorId, cmd.ConnectorMaxCurrent);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}