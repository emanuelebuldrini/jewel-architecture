using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class RemoveChargeStationCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<RemoveChargeStationCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(RemoveChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.Remove();

        await chargeStationRepo.RemoveAsync(chargeStation);
    }
}