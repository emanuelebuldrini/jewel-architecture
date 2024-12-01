using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class RemoveChargeStationCommandHandler(IRepository<ChargeStationAggregate, Guid> chargeStationRepo)
    : IAggregateCommandHandler<ChargeStationAggregate, Guid, RemoveChargeStationCommand>
{
    public async Task HandleAsync(RemoveChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.Remove();

        await chargeStationRepo.RemoveAsync(chargeStation);
    }
}