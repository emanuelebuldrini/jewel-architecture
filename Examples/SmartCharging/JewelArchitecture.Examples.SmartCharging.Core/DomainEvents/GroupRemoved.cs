namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents
{
    public record GroupRemoved(Guid GroupId) : IDomainEvent;    
}
