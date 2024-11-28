using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public class GroupChargeStationConnectorQueryHandler(IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
    IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler)
    : IQueryHandler<GroupChargeStationConnectorQuery, GroupChargeStationConnectorResult>
{
    public async Task<GroupChargeStationConnectorResult> HandleAsync(GroupChargeStationConnectorQuery query)
    {
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(new ChargeStationByIdQuery(query.ChargeStationId));
        var result = await groupConnectorQueryHandler.HandleAsync(new GroupConnectorQuery(chargeStation.Group.Id));

        return new GroupChargeStationConnectorResult(chargeStation, result);
    }
}
