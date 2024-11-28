using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface ILockService<TAggregate>
    where TAggregate : AggregateRootBase
{
    Task<ILock> AcquireLockAsync();
}
