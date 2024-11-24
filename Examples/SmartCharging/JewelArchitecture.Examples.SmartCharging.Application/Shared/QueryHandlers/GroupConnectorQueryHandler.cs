using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public class GroupConnectorQueryHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler)
    : IQueryHandler<GroupConnectorQuery, GroupConnectorResult>
{
    public async Task<GroupConnectorResult> HandleAsync(GroupConnectorQuery query)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(query.GroupId));

        var connectors = new List<ChargeStationConnectorEntity>();
        foreach (var chargeStationRef in group.ChargeStations)
        {
            var chargeStationQuery = new ChargeStationByIdQuery(chargeStationRef.Id);
            var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(chargeStationQuery);

            foreach (var connector in chargeStation.Connectors)
            {
                connectors.Add(connector);
            }
        }

        return new GroupConnectorResult(group, connectors.AsReadOnly());
    }
}
