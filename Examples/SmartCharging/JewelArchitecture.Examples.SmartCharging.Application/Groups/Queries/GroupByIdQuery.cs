using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;

public record GroupByIdQuery(Guid GroupId) : IQuery;