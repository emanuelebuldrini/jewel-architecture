using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;

public class UpdateMaxCurrentCommandHandler(IRepository<ChargeStationAggregate, Guid> chargeStationRepo)
    : IAggregateCommandHandler<ChargeStationAggregate, Guid, UpdateMaxCurrentCommand>
{
    public async Task HandleAsync(UpdateMaxCurrentCommand cmd)
    {
        var chargeStation = cmd.Aggregate;
        chargeStation.UpdateConnector(cmd.ConnectorId, cmd.MaxCurrent);

        await chargeStationRepo.AddOrReplaceAsync(chargeStation);
    }
}