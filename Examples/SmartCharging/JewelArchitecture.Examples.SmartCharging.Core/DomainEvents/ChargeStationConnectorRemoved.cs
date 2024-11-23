using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

public record ChargeStationConnectorRemoved(Guid ChargeStationId, GroupReference Group, ConnectorId ConnectorId) : IDomainEvent;
