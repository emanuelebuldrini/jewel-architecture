using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public class ChangeGroupChargeStationConnectorQueryHandler(IAggregateByIdQueryHandler<ChargeStationAggregate, Guid, AggregateByIdQuery<ChargeStationAggregate, Guid>> chargeStationByIdQueryHandler,
    IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler)
    : IQueryHandler<ChangeGroupChargeStationConnectorQuery, GroupChargeStationConnectorResult>
{
    public async Task<GroupChargeStationConnectorResult> HandleAsync(ChangeGroupChargeStationConnectorQuery query)
    {
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(new AggregateByIdQuery<ChargeStationAggregate, Guid>(query.ChargeStationId));
        var result = await groupConnectorQueryHandler.HandleAsync(new GroupConnectorQuery(query.GroupId));

        return new GroupChargeStationConnectorResult(chargeStation, result);
    }
}
