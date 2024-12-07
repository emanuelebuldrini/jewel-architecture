using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
}
