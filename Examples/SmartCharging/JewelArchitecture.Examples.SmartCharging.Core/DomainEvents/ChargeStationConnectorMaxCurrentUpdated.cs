using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents
{
    public record ChargeStationConnectorMaxCurrentUpdated(Guid ChargeStationId, ConnectorId ConnectorId, AmpereUnit MaxCurrent) 
        : IDomainEvent;
}
