using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

public class ChargeStationByIdQueryHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate>
{
    public async Task<ChargeStationAggregate> HandleAsync(ChargeStationByIdQuery query)
    {
        return await chargeStationRepo.GetSingleAsync(query.ChargeStationId);
    }
}
