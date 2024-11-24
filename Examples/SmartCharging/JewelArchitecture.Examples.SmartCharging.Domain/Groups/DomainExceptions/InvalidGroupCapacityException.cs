namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainExceptions;

public class InvalidGroupCapacityException(string? exceptionDetails)
    : Exception($"Unable to set the Group capacity due to a business constraint violation. {exceptionDetails}");
