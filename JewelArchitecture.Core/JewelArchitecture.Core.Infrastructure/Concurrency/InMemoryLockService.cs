using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Domain.BaseTypes;

namespace JewelArchitecture.Core.Infrastructure.Concurrency
{
    public class InMemoryLockService<TAggregate, TId>(int msTimeout = 20000) : ILockService<TAggregate, TId>, IDisposable
        where TAggregate : AggregateRootBase<TId> where TId : notnull 
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<ILock> AcquireLockAsync()
        {
            var lockAcquired = await _semaphore.WaitAsync(TimeSpan.FromMilliseconds(msTimeout));

            if (!lockAcquired)
            {
                throw new ApplicationException($"Unable to acquire a lock for '{typeof(TAggregate)}' " +
                    $"within a timeout of {msTimeout} ms.");
            }

            return new InMemoryLock(_semaphore);
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }
    }
}
