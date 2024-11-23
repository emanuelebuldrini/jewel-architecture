using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

public record ChargeStationCreated(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
