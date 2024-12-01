﻿using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Queries;

public record AggregateExistsQuery<TAggregate, TId>(TId AggregateId) : IAggregateQuery<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;