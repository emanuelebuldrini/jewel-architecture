using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents
{
    public record GroupRemoved(Guid GroupId) : IDomainEvent;
}
