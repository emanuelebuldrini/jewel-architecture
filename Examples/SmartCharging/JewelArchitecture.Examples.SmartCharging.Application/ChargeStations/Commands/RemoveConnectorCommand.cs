using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;

public record RemoveConnectorCommand<TAggregate>(TAggregate Aggregate, ConnectorId ConnectorId)
    : IAggregateCommand<TAggregate, Guid>
    where TAggregate:ChargeStationAggregate, IAggregateRoot<Guid>;
