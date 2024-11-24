using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class ChangeGroupCommandHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IAggregateCommandHandler<ChangeGroupCommand, ChargeStationAggregate>
{
    public async Task HandleAsync(ChangeGroupCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.ChangeGroup(cmd.GroupId);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}