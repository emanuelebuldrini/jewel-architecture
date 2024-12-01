﻿using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Decorators;

public sealed class AddOrReplaceAggregateCommandEventDispatcher<TAggregate, TId>
    (AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
    AddOrReplaceAggregateCommandHandler<TAggregate, TId> decoratee)
   : AggregateEventDispatcherDecoratorBase<TAggregate, TId, AddOrReplaceAggregateCommand<TAggregate, TId>>
    (decoratee, dispatcherService), IAddOrReplaceAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    public async Task HandleAsync(AddOrReplaceAggregateCommand<TAggregate, TId> cmd) =>
        await Dispatch(cmd);
}
