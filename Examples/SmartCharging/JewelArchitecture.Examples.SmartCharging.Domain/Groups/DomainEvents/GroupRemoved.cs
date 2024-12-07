using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents
{
    public record GroupRemoved(Guid GroupId) : IDomainEvent;
}
