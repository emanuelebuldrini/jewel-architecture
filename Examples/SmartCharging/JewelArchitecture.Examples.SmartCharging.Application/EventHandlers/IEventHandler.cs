using JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

namespace JewelArchitecture.Examples.SmartCharging.Application.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent: IDomainEvent
    {
        Task HandleAsync(TEvent e);
    }
}
