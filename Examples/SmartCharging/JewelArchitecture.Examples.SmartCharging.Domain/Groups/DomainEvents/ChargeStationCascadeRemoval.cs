using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents
{
    public record ChargeStationCascadeRemoval(Guid ChargeStationId) : IDomainEvent;
}
