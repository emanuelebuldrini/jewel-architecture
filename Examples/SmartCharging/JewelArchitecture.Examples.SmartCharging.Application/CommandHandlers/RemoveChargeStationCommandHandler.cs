using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class RemoveChargeStationCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<RemoveChargeStationCommand,ChargeStationAggregate>
{
    public async Task HandleAsync(RemoveChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.Remove();

        await chargeStationRepo.RemoveAsync(chargeStation);
    }
}