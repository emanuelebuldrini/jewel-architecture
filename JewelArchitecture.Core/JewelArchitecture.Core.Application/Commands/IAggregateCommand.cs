﻿using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Commands;

public interface IAggregateCommand<TAggregate, TId> : ICommand
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    TAggregate Aggregate { get; init; }
}