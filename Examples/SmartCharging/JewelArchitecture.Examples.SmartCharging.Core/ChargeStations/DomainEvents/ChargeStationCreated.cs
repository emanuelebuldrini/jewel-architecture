using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents;

public record ChargeStationCreated(Guid ChargeStationId, GroupReference Group) : IDomainEvent;
