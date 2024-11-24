using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents
{
    public record ChargeStationConnectorAdded(Guid ChargeStationId, ConnectorId ConnectorId, AmpereUnit MaxCurrent)
        : IDomainEvent;
}
