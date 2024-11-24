using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationConnectorRemoved(Guid ChargeStationId, GroupReference Group, ConnectorId ConnectorId) : IDomainEvent;
