using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Persistence;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared.Concurrency
{
    internal class SlowWriteInMemoryRepositoryMock<TAggregate>(InMemoryJsonRepository<TAggregate> inMemoryRepo,
        int addOrReplaceMsDelay, int removeMsDelay = 5, ConcurrencySynchronizer? startWriteSignal = null)
        : IRepository<TAggregate>
        where TAggregate : AggregateRootBase
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

        public async Task<bool> ExistsAsync(Guid aggregateId)
        {
            return await inMemoryRepo.ExistsAsync(aggregateId);
        }

        public async Task<TAggregate> GetSingleAsync(Guid aggregateId)
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
