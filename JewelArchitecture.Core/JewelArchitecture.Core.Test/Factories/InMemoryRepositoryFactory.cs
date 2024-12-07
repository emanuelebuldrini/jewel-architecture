using JewelArchitecture.Core.Domain.BaseTypes;
using JewelArchitecture.Core.Infrastructure.Persistence;
using JewelArchitecture.Core.Test.Concurrency;

namespace JewelArchitecture.Core.Test.Factories
{
    public static class InMemoryRepositoryFactory
    {
        public static InMemoryJsonRepository<TAggregate, TId> GetInMemoryRepository<TAggregate, TId>()
            where TAggregate : AggregateRootBase<TId> where TId : notnull  =>
            new(new AggregateJsonSerializer<TAggregate, TId>());

        public static SlowWriteInMemoryRepositoryMock<TAggregate, TId> GetSlowWriteInMemoryRepository<TAggregate, TId>(int addOrReplaceMsDelay = 200,
            int removeMsDelay = 5, ConcurrencySynchronizer? startWriteSignal = null)
          where TAggregate : AggregateRootBase<TId> where TId : notnull  => new(GetInMemoryRepository<TAggregate, TId>(), addOrReplaceMsDelay, removeMsDelay, startWriteSignal);
    }
}
