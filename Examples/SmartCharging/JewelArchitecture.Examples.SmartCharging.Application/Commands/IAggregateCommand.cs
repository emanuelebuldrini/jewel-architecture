using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public interface IAggregateCommand<TAggregate>: ICommand
    where TAggregate : AggregateRootBase
{
    TAggregate Aggregate { get; init; }
}
