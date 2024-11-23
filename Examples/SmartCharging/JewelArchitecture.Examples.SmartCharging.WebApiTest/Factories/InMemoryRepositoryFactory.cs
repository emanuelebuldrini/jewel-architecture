using JewelArchitecture.Examples.SmartCharging.Infrastructure.Serializers;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Repositories;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Mocks;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Factories
{
    internal static class InMemoryRepositoryFactory
    {
        public static InMemoryJsonRepository<TAggregate> GetInMemoryRepository<TAggregate>()
            where TAggregate : AggregateRootBase =>
            new(new AggregateJsonSerializer<TAggregate>());

        public static SlowWriteInMemoryRepositoryMock<TAggregate> GetSlowWriteInMemoryRepository<TAggregate>(int addOrReplaceMsDelay = 200,
            int removeMsDelay = 5, ConcurrencySynchronizer? startWriteSignal = null)
          where TAggregate : AggregateRootBase => new(GetInMemoryRepository<TAggregate>(), addOrReplaceMsDelay, removeMsDelay, startWriteSignal);
    }
}
