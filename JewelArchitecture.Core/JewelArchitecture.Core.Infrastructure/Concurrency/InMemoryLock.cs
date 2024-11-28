using JewelArchitecture.Core.Application.Abstractions;

namespace JewelArchitecture.Core.Infrastructure.Concurrency
{
    public class InMemoryLock(SemaphoreSlim semaphore) : ILock
    {
        public void Dispose()
        {
            Release();
        }

        public void Release()
        {
            semaphore.Release();
        }
    }
}
