using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;

public interface IAggregateCommand<TAggregate> : ICommand
    where TAggregate : AggregateRootBase
{
    TAggregate Aggregate { get; init; }
}
