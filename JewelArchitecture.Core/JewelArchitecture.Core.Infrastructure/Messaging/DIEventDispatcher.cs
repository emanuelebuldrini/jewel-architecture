using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Core.Infrastructure.Messaging
{
    public class DIEventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
    {
        public async Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            // Get registered handlers via DI to dispatch the domain event.
            var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(domainEvent);
            }
        }
    }
}
