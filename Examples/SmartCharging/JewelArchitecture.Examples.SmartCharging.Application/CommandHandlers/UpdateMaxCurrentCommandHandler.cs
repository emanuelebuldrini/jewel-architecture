using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

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