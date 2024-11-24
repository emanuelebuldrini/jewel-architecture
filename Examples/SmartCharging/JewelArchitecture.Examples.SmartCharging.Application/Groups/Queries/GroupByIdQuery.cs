using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;

public record GroupByIdQuery(Guid GroupId) : IQuery;