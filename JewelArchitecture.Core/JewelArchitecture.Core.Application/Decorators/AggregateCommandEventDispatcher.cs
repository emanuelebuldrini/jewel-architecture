﻿using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Decorators;

public class AggregateCommandEventDispatcher<TAggregate, TId, TCommand>(AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
    ICommandHandler<TCommand> decoratee)
    : IAggregateCommandHandler<TAggregate, TId, TCommand>
    where TAggregate : IAggregateRoot<TId>
    where TCommand: IAggregateCommand<TAggregate, TId>
    where TId : notnull
{
    public async Task HandleAsync(TCommand cmd)
    {
        await decoratee.HandleAsync(cmd);

        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);
    }
}