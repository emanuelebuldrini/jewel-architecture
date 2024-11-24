namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainExceptions;

public class ConnectorIdNotUniqueException(ConnectorId connectorId)
    : Exception($"Unable to add connector with ID '{connectorId.Value}' because already exists.");
