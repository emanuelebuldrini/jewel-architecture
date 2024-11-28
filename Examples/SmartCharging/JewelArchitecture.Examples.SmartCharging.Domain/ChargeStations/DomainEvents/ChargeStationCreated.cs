using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;

public record ChargeStationCreated(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
