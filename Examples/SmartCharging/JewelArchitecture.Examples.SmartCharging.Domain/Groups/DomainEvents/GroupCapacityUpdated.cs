using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents
{
    public record GroupCapacityUpdated(Guid GroupId, AmpereUnit Capacity) : IDomainEvent;
}
