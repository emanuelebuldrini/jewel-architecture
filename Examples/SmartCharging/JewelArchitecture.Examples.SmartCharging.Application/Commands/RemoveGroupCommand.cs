using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;