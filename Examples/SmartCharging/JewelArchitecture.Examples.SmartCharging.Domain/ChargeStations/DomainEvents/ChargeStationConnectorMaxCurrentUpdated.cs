using JewelArchitecture.Core.Domain.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents
{
    public record ChargeStationConnectorMaxCurrentUpdated(Guid ChargeStationId, ConnectorId ConnectorId, AmpereUnit MaxCurrent)
        : IDomainEvent;
}
