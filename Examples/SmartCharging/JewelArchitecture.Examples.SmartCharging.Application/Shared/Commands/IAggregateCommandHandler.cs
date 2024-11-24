﻿using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;

public interface IAggregateCommandHandler<TCommand, TAggregate> :
    ICommandHandler<TCommand>
    where TCommand : IAggregateCommand<TAggregate>
    where TAggregate : AggregateRootBase;