using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Domain;
using System.Collections.Concurrent;

namespace JewelArchitecture.Core.Infrastructure.Persistence
{
    public class InMemoryJsonRepository<TAggregate, TId>(AggregateJsonSerializer<TAggregate, TId> serializer)
        : IRepository<TAggregate, TId>
        where TAggregate : IAggregateRoot<TId>
        where TId : notnull 
    {
        private readonly ConcurrentDictionary<TId, string> _aggregateStore = new();

        public async Task AddOrReplaceAsync(TAggregate aggregate)
        {
            _aggregateStore[aggregate.Id] = await serializer.SerializeAsync(aggregate);
        }

        public Task<bool> ExistsAsync(TId aggregateId) => Task.FromResult(_aggregateStore.ContainsKey(aggregateId));

        public async Task<TAggregate> GetSingleAsync(TId aggregateId) =>
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
