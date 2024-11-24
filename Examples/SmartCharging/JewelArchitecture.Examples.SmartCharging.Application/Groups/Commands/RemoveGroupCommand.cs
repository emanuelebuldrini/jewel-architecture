using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;

public record RemoveGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;