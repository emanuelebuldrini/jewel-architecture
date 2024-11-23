using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveConnectorCommand(ChargeStationAggregate Aggregate, ConnectorId ConnectorId)
    : IAggregateCommand<ChargeStationAggregate>;