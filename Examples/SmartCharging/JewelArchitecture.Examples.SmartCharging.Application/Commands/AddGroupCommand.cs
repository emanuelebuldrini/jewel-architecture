using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record AddGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;
