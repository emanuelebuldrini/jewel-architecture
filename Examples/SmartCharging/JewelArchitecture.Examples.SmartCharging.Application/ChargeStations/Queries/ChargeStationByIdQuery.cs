
using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;

public record ChargeStationByIdQuery(Guid ChargeStationId) : IQuery;