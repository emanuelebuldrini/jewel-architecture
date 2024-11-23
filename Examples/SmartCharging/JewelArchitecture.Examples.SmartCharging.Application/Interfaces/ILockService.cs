using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Interfaces
{
    public interface ILockService<TAggregate>
        where TAggregate : AggregateRootBase
    {
        Task<ILock> AcquireLockAsync();
    }
}
