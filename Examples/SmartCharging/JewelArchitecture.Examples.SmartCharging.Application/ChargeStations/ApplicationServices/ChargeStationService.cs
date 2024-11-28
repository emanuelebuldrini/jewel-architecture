using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Application.Abstractions;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;

public class ChargeStationService(ILockService<ChargeStationAggregate> chargeStationLockService,
    IAggregateExistsQueryHandler<ChargeStationAggregate, AggregateExistsQuery<ChargeStationAggregate>> chargeStationExistsQueryHandler,
    IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
    ICommandHandler<AddOrReplaceChargeStationCommand> addOrReplaceChargeStationCommandHandler
)
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        var query = new AggregateExistsQuery<ChargeStationAggregate>(id);
        return await chargeStationExistsQueryHandler.HandleAsync(query);
    }

    public async Task<ChargeStationAggregate> GetSingleAsync(Guid chargeStationId)
    {
        return await chargeStationByIdQueryHandler.HandleAsync(new ChargeStationByIdQuery(chargeStationId));
    }

    public async Task EditAsync(Guid chargeStationId, ChargeStationEditDto editDto)
    {
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        var chargeStation = await GetSingleAsync(chargeStationId);

        chargeStation.Name = editDto.Name;

        await addOrReplaceChargeStationCommandHandler.HandleAsync(new AddOrReplaceChargeStationCommand(chargeStation));
    }
}
