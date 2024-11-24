using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents
{
    public record GroupCapacityUpdated(Guid GroupId, AmpereUnit Capacity) : IDomainEvent;
}
