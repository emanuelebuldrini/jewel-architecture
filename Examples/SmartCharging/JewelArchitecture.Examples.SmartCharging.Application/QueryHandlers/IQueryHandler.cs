using JewelArchitecture.Examples.SmartCharging.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

public interface IQueryHandler<TQuery,TResult>
    where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}