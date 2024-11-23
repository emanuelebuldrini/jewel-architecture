using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;

public record GroupConnectorResult(GroupAggregate Group, IReadOnlyCollection<ChargeStationConnectorEntity> Connectors);