using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface IRepository<TAggregate, TId> 
    where TAggregate : IAggregateRoot<TId> 
    where TId : notnull
{
    public Task AddOrReplaceAsync(TAggregate aggregate);
    public Task<TAggregate> GetSingleAsync(TId aggregateId);
    public Task<bool> ExistsAsync(TId aggregateId);
    public Task RemoveAsync(TAggregate aggregate);
}
