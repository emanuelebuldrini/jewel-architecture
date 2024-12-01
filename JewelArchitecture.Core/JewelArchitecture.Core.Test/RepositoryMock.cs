using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Test
{
    public class RepositoryMock<TAggregate, TId>() : IRepository<TAggregate, TId>
        where TAggregate : AggregateRootBase<TId>
        where TId : notnull 
    {
        private readonly List<TAggregate> _aggregates = [];

        public IReadOnlyCollection<TAggregate> Aggregates => _aggregates.AsReadOnly();

        /// <summary>
        /// Inizialize the repo with a collection.
        /// </summary>
        /// <param name="aggregates">The collection to add to the repo.</param>
        public RepositoryMock(TAggregate[] aggregates) : this()
        {
            foreach (var aggregate in aggregates)
            {
                // Add a shallow copy to avoid that external object modifications impact the mock repo.
                _aggregates.Add(aggregate with { });
            }
        }

        public Task<bool> ExistsAsync(TId aggregateId) =>
            Task.FromResult(_aggregates.Any(g => g.Id.Equals(aggregateId)));

        public async Task AddOrReplaceAsync(TAggregate modifiedAggregate)
        {
            // Create a shallow copy to avoid that external object modifications impact the mock repo.
            var aggregateState = modifiedAggregate with { };

            if (!await ExistsAsync(modifiedAggregate.Id))
            {
                // Add logic
                _aggregates.Add(aggregateState);
            }
            else
            {
                // Replace logic
                var existingAggregate = _aggregates.Single(s => s.Id.Equals(modifiedAggregate.Id));
                var existingAggregateIndex = _aggregates.IndexOf(existingAggregate);
                _aggregates[existingAggregateIndex] = aggregateState;
            }
        }

        public Task<TAggregate> GetSingleAsync(TId aggregateId) =>
            Task.FromResult(_aggregates.Single(s => s.Id.Equals(aggregateId)) with { });

        public Task RemoveAsync(TAggregate aggregate)
        {
            _aggregates.Remove(aggregate);
            return Task.CompletedTask;
        }
    }
}
