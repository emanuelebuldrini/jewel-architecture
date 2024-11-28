using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Commands;

public interface IAggregateCommand<TAggregate> : ICommand
    where TAggregate : AggregateRootBase
{
    TAggregate Aggregate { get; init; }
}
