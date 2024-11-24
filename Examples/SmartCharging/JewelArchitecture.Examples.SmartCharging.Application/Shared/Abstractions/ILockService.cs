﻿using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Interfaces
{
    public interface ILockService<TAggregate>
        where TAggregate : AggregateRootBase
    {
        Task<ILock> AcquireLockAsync();
    }
}
