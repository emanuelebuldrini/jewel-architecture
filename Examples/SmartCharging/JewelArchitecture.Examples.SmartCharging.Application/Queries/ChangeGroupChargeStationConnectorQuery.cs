namespace JewelArchitecture.Examples.SmartCharging.Application.Queries;

public record ChangeGroupChargeStationConnectorQuery(Guid ChargeStationId, Guid GroupId) : IQuery;