using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Core.Application.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.QueryHandlers;

public class ChargeStationByIdQueryHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate>
{
    public async Task<ChargeStationAggregate> HandleAsync(ChargeStationByIdQuery query)
    {
        return await chargeStationRepo.GetSingleAsync(query.ChargeStationId);
    }
}
