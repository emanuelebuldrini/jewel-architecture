using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public class GroupConnectorQueryHandler( IAggregateByIdQueryHandler<GroupAggregate, Guid, AggregateByIdQuery<GroupAggregate, Guid>> groupByIdQueryHandler,
    IAggregateByIdQueryHandler<ChargeStationAggregate, Guid, AggregateByIdQuery<ChargeStationAggregate, Guid>> chargeStationByIdQueryHandler)
    : IQueryHandler<GroupConnectorQuery, GroupConnectorResult>
{
    public async Task<GroupConnectorResult> HandleAsync(GroupConnectorQuery query)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new AggregateByIdQuery<GroupAggregate,Guid>(query.GroupId));

        var connectors = new List<ChargeStationConnectorEntity>();
        foreach (var chargeStationRef in group.ChargeStations)
        {
            var chargeStationQuery = new AggregateByIdQuery<ChargeStationAggregate, Guid>(chargeStationRef.Id);
            var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(chargeStationQuery);

            foreach (var connector in chargeStation.Connectors)
            {
                connectors.Add(connector);
            }
        }

        return new GroupConnectorResult(group, connectors.AsReadOnly());
    }
}
