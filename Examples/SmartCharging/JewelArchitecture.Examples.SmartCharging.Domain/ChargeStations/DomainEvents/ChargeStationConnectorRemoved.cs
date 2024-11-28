using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationConnectorRemoved(Guid ChargeStationId, GroupReference Group, ConnectorId ConnectorId) : IDomainEvent;
