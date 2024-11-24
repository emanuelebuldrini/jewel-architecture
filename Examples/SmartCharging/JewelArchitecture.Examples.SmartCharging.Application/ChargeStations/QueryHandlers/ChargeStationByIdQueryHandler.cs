using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.QueryHandlers;

public class ChargeStationByIdQueryHandler(IRepository<ChargeStationAggregate> chargeStationRepo)
    : IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate>
{
    public async Task<ChargeStationAggregate> HandleAsync(ChargeStationByIdQuery query)
    {
        return await chargeStationRepo.GetSingleAsync(query.ChargeStationId);
    }
}
