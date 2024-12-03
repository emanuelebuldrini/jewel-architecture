using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using System.Collections.ObjectModel;

namespace JewelArchitecture.Examples.SmartCharging.Test.Shared.Factories;

internal class AggregateFactory
{
    internal static ChargeStationAggregate CreateChargeStationWithTwoConnectors(int connectorNumber1, int maxCurrentAmps1, int connectorNumber2, int maxCurrentAmps2, GroupAggregate group)
    {
        List<(ConnectorId, AmpereUnit)> connectors = [
             (new ConnectorId(connectorNumber1), new AmpereUnit(maxCurrentAmps1)),
            (new ConnectorId(connectorNumber2), new AmpereUnit(maxCurrentAmps2))
         ];

        var chargeStation1 = CreateChargeStation(group, connectors.AsReadOnly());

        group.ChargeStations.Add(new ChargeStationReference(chargeStation1.Id));

        return chargeStation1;
    }

    internal static ChargeStationAggregate CreateChargeStationWithOneConnector(int connectorNumber1, int maxCurrentAmps1, GroupAggregate group)
    {
        List<(ConnectorId, AmpereUnit)> connectors = [
            (new ConnectorId(connectorNumber1), new AmpereUnit(maxCurrentAmps1))
        ];

        var chargeStation1 = CreateChargeStation(group, connectors.AsReadOnly());

        group.ChargeStations.Add(new ChargeStationReference(chargeStation1.Id));

        return chargeStation1;
    }

    internal static ChargeStationAggregate CreateChargeStationWithThreeConnectors(int connectorNumber1, int maxCurrentAmps1, int connectorNumber2, int maxCurrentAmps2, int connectorNumber3, int maxCurrentAmps3, GroupAggregate group)
    {
        List<(ConnectorId, AmpereUnit)> connectors = [
            (new ConnectorId(connectorNumber1), new AmpereUnit(maxCurrentAmps1)),
            (new ConnectorId(connectorNumber2), new AmpereUnit(maxCurrentAmps2)),
            (new ConnectorId(connectorNumber3), new AmpereUnit(maxCurrentAmps3))
        ];

        var chargeStation1 = CreateChargeStation(group, connectors.AsReadOnly());

        group.ChargeStations.Add(new ChargeStationReference(chargeStation1.Id));

        return chargeStation1;
    }

    private static ChargeStationAggregate CreateChargeStation(GroupAggregate group,
        ReadOnlyCollection<(ConnectorId, AmpereUnit)> connectors)
    {
        var aggregate = ChargeStationAggregate.Create("Charge Station 1",
            connectors.AsReadOnly(), new GroupReference(group.Id));
        // Clean up creation events because they are not managed in this test scenario setup.
        aggregate.ClearEvents();
        return aggregate;
    }
}
