using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Core.DomainExceptions;

public class ConnectorIdNotUniqueException(ConnectorId connectorId) 
    : Exception($"Unable to add connector with ID '{connectorId.Value}' because already exists.");
