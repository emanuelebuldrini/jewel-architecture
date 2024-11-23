namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainExceptions;

public class ExceedsGroupCapacityException(string? exceptionDetails)
    : Exception($"This operation violates a business constraint on Group capacity. {exceptionDetails}");
