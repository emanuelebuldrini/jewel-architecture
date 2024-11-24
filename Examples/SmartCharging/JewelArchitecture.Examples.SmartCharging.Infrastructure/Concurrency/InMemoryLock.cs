using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;

namespace JewelArchitecture.Examples.SmartCharging.Infrastructure.Concurrency
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
