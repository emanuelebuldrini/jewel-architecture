using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;

public record RemoveConnectorCommand(ChargeStationAggregate Aggregate, ConnectorId ConnectorId)
    : IAggregateCommand<ChargeStationAggregate>;