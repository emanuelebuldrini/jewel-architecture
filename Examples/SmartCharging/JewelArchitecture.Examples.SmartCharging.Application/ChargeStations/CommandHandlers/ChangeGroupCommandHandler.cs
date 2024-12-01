using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class ChangeGroupCommandHandler(IRepository<ChargeStationAggregate, Guid> chargeStationRepo)
    : IAggregateCommandHandler<ChargeStationAggregate, Guid, ChangeGroupCommand>
{
    public async Task HandleAsync(ChangeGroupCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.ChangeGroup(cmd.GroupId);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}