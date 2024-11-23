using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Interfaces;

public interface IRepository<TAggregate> where TAggregate : AggregateRootBase
{
    public Task AddOrReplaceAsync(TAggregate aggregate);
    public Task<TAggregate> GetSingleAsync(Guid aggregateId);
    public Task<bool> ExistsAsync(Guid aggregateId);
    public Task RemoveAsync(TAggregate aggregate);
}
