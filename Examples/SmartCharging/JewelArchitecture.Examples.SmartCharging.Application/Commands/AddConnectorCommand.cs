using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record AddConnectorCommand(ConnectorId ConnectorId, AmpereUnit ConnectorMaxCurrent, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
