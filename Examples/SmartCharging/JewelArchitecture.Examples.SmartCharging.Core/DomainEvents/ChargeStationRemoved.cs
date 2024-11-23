using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

public record ChargeStationRemoved(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
