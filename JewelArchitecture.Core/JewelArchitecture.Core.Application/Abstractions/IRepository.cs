using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface IRepository<TAggregate> where TAggregate : AggregateRootBase
{
    public Task AddOrReplaceAsync(TAggregate aggregate);
    public Task<TAggregate> GetSingleAsync(Guid aggregateId);
    public Task<bool> ExistsAsync(Guid aggregateId);
    public Task RemoveAsync(TAggregate aggregate);
}
