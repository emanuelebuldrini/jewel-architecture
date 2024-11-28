using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

public record GroupChargeStationConnectorQuery(Guid ChargeStationId) : IQuery;