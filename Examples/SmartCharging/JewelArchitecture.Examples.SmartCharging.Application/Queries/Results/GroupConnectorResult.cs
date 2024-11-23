using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.Entities;

namespace JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;

public record GroupConnectorResult(GroupAggregate Group, IReadOnlyCollection<ChargeStationConnectorEntity> Connectors);