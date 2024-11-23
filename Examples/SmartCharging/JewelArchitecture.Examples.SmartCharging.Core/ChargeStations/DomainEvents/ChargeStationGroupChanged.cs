using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents;

public record ChargeStationGroupChanged(Guid ChargeStationId, GroupReference OldGroup,
    GroupReference NewGroup) : IDomainEvent;
