using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;

public class ChargeStationService(ILockService<ChargeStationAggregate> chargeStationLockService,
    IAggregateExistsQueryHandler<ChargeStationAggregate, AggregateExistsQuery<ChargeStationAggregate>> chargeStationExistsQueryHandler,
    IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
    ICommandHandler<AddChargeStationCommand> addOrReplaceChargeStationCommandHandler
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

        await addOrReplaceChargeStationCommandHandler.HandleAsync(new AddChargeStationCommand(chargeStation));
    }
}
