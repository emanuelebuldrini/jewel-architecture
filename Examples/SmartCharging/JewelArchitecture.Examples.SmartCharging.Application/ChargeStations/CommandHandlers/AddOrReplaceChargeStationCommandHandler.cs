using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class AddOrReplaceChargeStationCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<AddOrReplaceChargeStationCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(AddOrReplaceChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}