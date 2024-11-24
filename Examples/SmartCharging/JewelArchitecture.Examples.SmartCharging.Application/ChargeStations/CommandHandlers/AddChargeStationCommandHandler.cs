using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class AddChargeStationCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<AddChargeStationCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(AddChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}