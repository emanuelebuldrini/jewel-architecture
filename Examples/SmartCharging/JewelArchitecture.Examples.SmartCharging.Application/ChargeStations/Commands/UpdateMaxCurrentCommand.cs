using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;

public record UpdateMaxCurrentCommand(ConnectorId ConnectorId, AmpereUnit MaxCurrent, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
