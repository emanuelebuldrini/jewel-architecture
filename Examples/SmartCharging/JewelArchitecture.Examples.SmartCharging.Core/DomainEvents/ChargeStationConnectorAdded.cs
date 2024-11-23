using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainEvents
{
    public record ChargeStationConnectorAdded(Guid ChargeStationId, ConnectorId ConnectorId, AmpereUnit MaxCurrent) 
        : IDomainEvent;
}
