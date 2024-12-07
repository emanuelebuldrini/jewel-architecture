using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationGroupChanged(Guid ChargeStationId, GroupReference OldGroup,
    GroupReference NewGroup) : IDomainEvent;
