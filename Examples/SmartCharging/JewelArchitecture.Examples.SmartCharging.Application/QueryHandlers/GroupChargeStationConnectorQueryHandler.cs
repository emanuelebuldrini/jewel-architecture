using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

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
