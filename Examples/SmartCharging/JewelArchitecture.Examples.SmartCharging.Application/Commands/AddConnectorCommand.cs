using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record AddConnectorCommand(ConnectorId ConnectorId, AmpereUnit ConnectorMaxCurrent, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
