using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Events;

public interface IEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent e);
}
