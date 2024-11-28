using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Abstractions;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
}
