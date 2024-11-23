using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record UpdateMaxCurrentCommand(ConnectorId ConnectorId, AmpereUnit MaxCurrent, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
