using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents
{
    public record GroupCapacityUpdated(Guid GroupId, AmpereUnit Capacity) : IDomainEvent;    
}
