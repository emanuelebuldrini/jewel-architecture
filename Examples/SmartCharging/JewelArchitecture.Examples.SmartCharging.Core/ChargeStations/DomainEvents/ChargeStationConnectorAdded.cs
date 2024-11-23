using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents
{
    public record ChargeStationConnectorAdded(Guid ChargeStationId, ConnectorId ConnectorId, AmpereUnit MaxCurrent)
        : IDomainEvent;
}
