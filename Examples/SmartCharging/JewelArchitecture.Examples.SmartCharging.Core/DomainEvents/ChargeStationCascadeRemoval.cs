namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents
{
    public record ChargeStationCascadeRemoval(Guid ChargeStationId) : IDomainEvent;   
}
