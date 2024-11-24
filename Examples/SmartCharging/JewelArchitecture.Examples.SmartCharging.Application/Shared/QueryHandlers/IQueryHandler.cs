using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}