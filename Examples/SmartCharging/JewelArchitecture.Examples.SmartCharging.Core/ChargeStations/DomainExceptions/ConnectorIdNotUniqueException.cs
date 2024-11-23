using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;

public class ConnectorIdNotUniqueException(ConnectorId connectorId)
    : Exception($"Unable to add connector with ID '{connectorId.Value}' because already exists.");
