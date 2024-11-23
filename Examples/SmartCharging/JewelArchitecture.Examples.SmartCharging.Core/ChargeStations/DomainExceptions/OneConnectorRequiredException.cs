namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;

public class OneConnectorRequiredException(string? exceptionDetails)
    : Exception($"A charge station must have at least a connector. {exceptionDetails}");
