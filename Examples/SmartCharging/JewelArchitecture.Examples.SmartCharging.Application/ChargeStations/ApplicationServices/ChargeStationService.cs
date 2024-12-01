using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;

public class ChargeStationService(ILockService<ChargeStationAggregate, Guid> chargeStationLockService,
    IAggregateExistsQueryHandler<ChargeStationAggregate, Guid, AggregateExistsQuery<ChargeStationAggregate, Guid>> chargeStationExistsQueryHandler,
    IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
    IAddOrReplaceAggregateCommandHandler<ChargeStationAggregate,Guid> addOrReplaceChargeStationCommandHandler
)
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        var query = new AggregateExistsQuery<ChargeStationAggregate, Guid>(id);
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

        await addOrReplaceChargeStationCommandHandler.HandleAsync(new AddAggregateCommand<ChargeStationAggregate, Guid>(chargeStation));
    }
}
