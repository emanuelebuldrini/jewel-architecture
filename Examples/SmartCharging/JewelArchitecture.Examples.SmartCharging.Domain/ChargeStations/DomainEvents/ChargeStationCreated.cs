using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationCreated(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
