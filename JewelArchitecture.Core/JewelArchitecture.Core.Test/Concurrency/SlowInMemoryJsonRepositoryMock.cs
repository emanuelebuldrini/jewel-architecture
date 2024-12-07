using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Domain.BaseTypes;
using JewelArchitecture.Core.Infrastructure.Persistence;

namespace JewelArchitecture.Core.Test.Concurrency
{
    public class SlowWriteInMemoryRepositoryMock<TAggregate, TId>(InMemoryJsonRepository<TAggregate, TId> inMemoryRepo,
        int addOrReplaceMsDelay, int removeMsDelay = 5, ConcurrencySynchronizer? startWriteSignal = null)
        : IRepository<TAggregate, TId>
        where TAggregate : AggregateRootBase<TId> where TId : notnull 
    {
        public async Task FastAddOrReplaceAsync(TAggregate aggregate)
        {
            await inMemoryRepo.AddOrReplaceAsync(aggregate);
        }

        public async Task AddOrReplaceAsync(TAggregate aggregate)
        {
            if (startWriteSignal?.IsDisposed == false)
            {
                await startWriteSignal.WaitAsync();
                // Simulate a slow write operation.
                await Task.Delay(addOrReplaceMsDelay);
            }

            await inMemoryRepo.AddOrReplaceAsync(aggregate);
        }

        public async Task<bool> ExistsAsync(TId aggregateId)
        {
            return await inMemoryRepo.ExistsAsync(aggregateId);
        }

        public async Task<TAggregate> GetSingleAsync(TId aggregateId)
        {
            return await inMemoryRepo.GetSingleAsync(aggregateId);
        }

        public async Task RemoveAsync(TAggregate aggregate)
        {
            if (startWriteSignal?.IsDisposed == false)
            {
                await startWriteSignal.WaitAsync();
                await Task.Delay(removeMsDelay);
            }

            await inMemoryRepo.RemoveAsync(aggregate);
        }
    }
}
