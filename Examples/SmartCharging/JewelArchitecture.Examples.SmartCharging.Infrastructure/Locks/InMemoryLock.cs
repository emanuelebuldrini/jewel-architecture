using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Infrastructure.Locks
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
