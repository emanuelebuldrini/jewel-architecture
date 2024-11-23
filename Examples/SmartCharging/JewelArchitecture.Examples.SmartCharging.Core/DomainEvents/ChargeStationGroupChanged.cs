using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

public record ChargeStationGroupChanged(Guid ChargeStationId, GroupReference OldGroup,
    GroupReference NewGroup) : IDomainEvent;
