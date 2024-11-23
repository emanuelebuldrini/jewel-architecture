namespace JewelArchitecture.Examples.SmartCharging.Core.DomainExceptions;

public class ExceedsGroupCapacityException(string? exceptionDetails) 
    : Exception($"This operation violates a business constraint on Group capacity. {exceptionDetails}");
