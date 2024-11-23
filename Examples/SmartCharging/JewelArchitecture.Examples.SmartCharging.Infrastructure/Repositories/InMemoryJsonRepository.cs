using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Serializers;
using System.Collections.Concurrent;

namespace JewelArchitecture.Examples.SmartCharging.Infrastructure.Repositories
{
    public class InMemoryJsonRepository<TAggregate>(AggregateJsonSerializer<TAggregate> serializer) 
        : IRepository<TAggregate>
        where TAggregate : AggregateRootBase
    {
        private readonly ConcurrentDictionary<Guid, string> _aggregateStore = new();

        public async Task AddOrReplaceAsync(TAggregate aggregate)
        {
            _aggregateStore[aggregate.Id] = await serializer.SerializeAsync(aggregate);
        }

        public Task<bool> ExistsAsync(Guid aggregateId) => Task.FromResult(_aggregateStore.ContainsKey(aggregateId));

        public async Task<TAggregate> GetSingleAsync(Guid aggregateId) =>
            await serializer.DeserializeAsync(_aggregateStore[aggregateId]);

        public Task RemoveAsync(TAggregate aggregate)
        {
            var keyNotFound = !_aggregateStore.Remove(aggregate.Id, out _);

            if (keyNotFound)
            {
                throw new InvalidOperationException($"Attempt to remove an aggregate not found in the repository of type '{typeof(TAggregate)}' " +
                    $"using the key '{aggregate.Id}'");
            }

            return Task.CompletedTask;
        }
    }
}
