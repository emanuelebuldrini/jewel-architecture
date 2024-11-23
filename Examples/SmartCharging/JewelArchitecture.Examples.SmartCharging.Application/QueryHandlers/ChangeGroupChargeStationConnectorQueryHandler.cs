using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

public class ChangeGroupChargeStationConnectorQueryHandler(IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
    IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler)
    : IQueryHandler<ChangeGroupChargeStationConnectorQuery, GroupChargeStationConnectorResult>
{
    public async Task<GroupChargeStationConnectorResult> HandleAsync(ChangeGroupChargeStationConnectorQuery query)
    {
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(new ChargeStationByIdQuery(query.ChargeStationId));
        var result = await groupConnectorQueryHandler.HandleAsync(new GroupConnectorQuery(query.GroupId));

        return new GroupChargeStationConnectorResult(chargeStation, result);        
    }
}
