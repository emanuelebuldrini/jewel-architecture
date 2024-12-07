using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationRemoved(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
