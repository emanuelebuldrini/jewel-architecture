using JewelArchitecture.Core.Application;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Infrastructure.Concurrency;
using JewelArchitecture.Core.Infrastructure.Messaging;
using JewelArchitecture.Core.Infrastructure.Persistence;
using JewelArchitecture.Core.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands.Decorators;

namespace JewelArchitecture.Core.Interface;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddJewelArchitecture(this IServiceCollection serviceCollection) {
        serviceCollection
           .AddSingleton<IEventDispatcher, DIEventDispatcher>()
           .AddSingleton(typeof(AggregateEventDispatcherService<,>))
           .AddSingleton(typeof(ILockService<,>), typeof(InMemoryLockService<,>))

           .Scan(scan => scan.FromApplicationDependencies()
               .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<>),
                   typeof(IQueryHandler<,>), typeof(IEventHandler<>))
               // Exclude decorators that should be added later to wrap decoratees.
               .NotInNamespaceOf(typeof(AggregateEventDispatcherDecoratorBase<,,>))
               )
               .AsImplementedInterfaces()
               .WithSingletonLifetime())

           // Add command decorators: should be registered after the command decoratees to wrap them.
           .AddSingleton(typeof(AddOrReplaceAggregateCommandHandler<,>)) // Add the concrete type to avoid loops in the decorator.
           .AddSingleton(typeof(IAddOrReplaceAggregateCommandHandler<,>), typeof(AddOrReplaceAggregateCommandEventDispatcher<,>))
           .AddSingleton(typeof(RemoveAggregateCommandHandler<,>))// Add the concrete type to avoid loops in the decorator.
           .AddSingleton(typeof(IRemoveAggregateCommandHandler<,>), typeof(RemoveAggregateCommandEventDispatcher<,>));
        serviceCollection
            .TryDecorate(typeof(IAggregateCommandHandler<,,>), typeof(AggregateCommandEventDispatcher<,,>));

        return serviceCollection;
    }
    // Add command decorators: should be registered after the command decoratees to wrap them.

    public static IServiceCollection AddInMemoryJsonRepository(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(typeof(IRepository<,>), typeof(InMemoryJsonRepository<,>))
            .AddSingleton(typeof(AggregateJsonSerializer<,>));
}
