using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents;

public record ChargeStationConnectorRemoved(Guid ChargeStationId, GroupReference Group, ConnectorId ConnectorId) : IDomainEvent;
