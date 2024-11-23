using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record UpdateMaxCurrentCommand(ConnectorId ConnectorId, AmpereUnit MaxCurrent, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
