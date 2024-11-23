using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Interfaces
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}
