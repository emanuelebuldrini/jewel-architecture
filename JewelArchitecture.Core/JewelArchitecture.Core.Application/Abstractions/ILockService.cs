using JewelArchitecture.Core.Domain.BaseTypes;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface ILockService<TAggregate, TId>
    where TAggregate : AggregateRootBase<TId> where TId : notnull 
{
    Task<ILock> AcquireLockAsync();
}
