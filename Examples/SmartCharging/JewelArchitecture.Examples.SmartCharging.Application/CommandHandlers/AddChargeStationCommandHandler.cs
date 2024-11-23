using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class AddChargeStationCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<AddChargeStationCommand,ChargeStationAggregate>
{
    public async Task HandleAsync(AddChargeStationCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        await chargeStationRepo.AddOrReplaceAsync(chargeStation);        
    }
}