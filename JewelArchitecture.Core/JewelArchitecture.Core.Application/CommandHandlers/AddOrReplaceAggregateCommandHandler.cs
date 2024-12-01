using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public class AddOrReplaceAggregateCommandHandler<TAggregate, TId>(IRepository<TAggregate, TId> repo)
    : IAddOrReplaceAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    public async Task HandleAsync(AddAggregateCommand<TAggregate, TId> cmd) =>
        await repo.AddOrReplaceAsync(cmd.Aggregate);
}