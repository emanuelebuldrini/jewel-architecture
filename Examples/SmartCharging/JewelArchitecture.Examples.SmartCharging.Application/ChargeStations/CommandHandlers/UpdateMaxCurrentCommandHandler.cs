using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class UpdateMaxCurrentCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<UpdateMaxCurrentCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(UpdateMaxCurrentCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.UpdateConnector(cmd.ConnectorId, cmd.MaxCurrent);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}