using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.Groups.DomainEvents
{
    public record GroupRemoved(Guid GroupId) : IDomainEvent;
}
