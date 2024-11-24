using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;

public record AddGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;
