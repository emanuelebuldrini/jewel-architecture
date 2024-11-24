using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;

public record GroupConnectorResult(GroupAggregate Group, IReadOnlyCollection<ChargeStationConnectorEntity> Connectors);