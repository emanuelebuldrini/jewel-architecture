using JewelArchitecture.Core.Domain;
using JewelArchitecture.Core.Infrastructure.Persistence;
using JewelArchitecture.Core.Test.Concurrency;

namespace JewelArchitecture.Core.Test.Factories
{
    public static class InMemoryRepositoryFactory
    {
        public static InMemoryJsonRepository<TAggregate> GetInMemoryRepository<TAggregate>()
            where TAggregate : AggregateRootBase =>
            new(new AggregateJsonSerializer<TAggregate>());

        public static SlowWriteInMemoryRepositoryMock<TAggregate> GetSlowWriteInMemoryRepository<TAggregate>(int addOrReplaceMsDelay = 200,
            int removeMsDelay = 5, ConcurrencySynchronizer? startWriteSignal = null)
          where TAggregate : AggregateRootBase => new(GetInMemoryRepository<TAggregate>(), addOrReplaceMsDelay, removeMsDelay, startWriteSignal);
    }
}
