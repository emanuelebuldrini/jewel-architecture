using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}