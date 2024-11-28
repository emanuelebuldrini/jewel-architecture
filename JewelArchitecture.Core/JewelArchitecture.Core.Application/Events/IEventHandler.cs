using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Events;

public interface IEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent e);
}
