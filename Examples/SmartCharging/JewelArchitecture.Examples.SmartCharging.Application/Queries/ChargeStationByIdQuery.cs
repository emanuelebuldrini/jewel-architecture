namespace JewelArchitecture.Examples.SmartCharging.Application.Queries;

public record ChargeStationByIdQuery(Guid ChargeStationId) : IQuery;