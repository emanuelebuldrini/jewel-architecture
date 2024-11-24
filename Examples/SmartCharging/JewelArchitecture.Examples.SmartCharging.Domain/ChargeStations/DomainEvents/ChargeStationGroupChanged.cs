using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationGroupChanged(Guid ChargeStationId, GroupReference OldGroup,
    GroupReference NewGroup) : IDomainEvent;
