using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

public record ChangeGroupChargeStationConnectorQuery(Guid ChargeStationId, Guid GroupId) : IQuery;