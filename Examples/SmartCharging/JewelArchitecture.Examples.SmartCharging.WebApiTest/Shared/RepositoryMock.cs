using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared
{
    internal class RepositoryMock<TAggregate>() : IRepository<TAggregate>
        where TAggregate : AggregateRootBase
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

        public Task<bool> ExistsAsync(Guid aggregateId) =>
            Task.FromResult(_aggregates.Any(g => g.Id == aggregateId));

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
                var existingAggregate = _aggregates.Single(s => s.Id == modifiedAggregate.Id);
                var existingAggregateIndex = _aggregates.IndexOf(existingAggregate);
                _aggregates[existingAggregateIndex] = aggregateState;
            }
        }

        public Task<TAggregate> GetSingleAsync(Guid aggregateId) =>
            Task.FromResult(_aggregates.Single(s => s.Id == aggregateId) with { });

        public Task RemoveAsync(TAggregate aggregate)
        {
            _aggregates.Remove(aggregate);
            return Task.CompletedTask;
        }
    }
}
