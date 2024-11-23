using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveConnectorCommand(ChargeStationAggregate Aggregate, ConnectorId ConnectorId)
    : IAggregateCommand<ChargeStationAggregate>;