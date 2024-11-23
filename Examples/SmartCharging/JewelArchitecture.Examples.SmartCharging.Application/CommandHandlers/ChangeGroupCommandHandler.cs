using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class ChangeGroupCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<ChangeGroupCommand,ChargeStationAggregate>
{
    public async Task HandleAsync(ChangeGroupCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.ChangeGroup(cmd.GroupId);
        
        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}