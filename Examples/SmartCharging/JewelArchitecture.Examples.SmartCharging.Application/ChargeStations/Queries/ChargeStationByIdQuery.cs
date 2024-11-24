using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;

public record ChargeStationByIdQuery(Guid ChargeStationId) : IQuery;