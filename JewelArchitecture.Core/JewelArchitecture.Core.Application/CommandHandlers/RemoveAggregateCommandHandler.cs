using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public class RemoveAggregateCommandHandler<TAggregate, TId>(IRepository<TAggregate, TId> repo)
    : IRemoveAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    public async Task HandleAsync(RemoveAggregateCommand<TAggregate, TId> cmd) =>
        await repo.RemoveAsync(cmd.Aggregate);
}