using JewelArchitecture.Examples.SmartCharging.Application.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.Infrastructure.Messaging
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
