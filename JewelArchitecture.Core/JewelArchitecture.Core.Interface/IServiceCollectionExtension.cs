using JewelArchitecture.Core.Application;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Application.Decorators;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Infrastructure.Concurrency;
using JewelArchitecture.Core.Infrastructure.Messaging;
using JewelArchitecture.Core.Infrastructure.Persistence;
using JewelArchitecture.Core.Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Core.Interface;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddJewelArchitecture(this IServiceCollection serviceCollection) =>
         serviceCollection
            .AddSingleton<IEventDispatcher, DIEventDispatcher>()
            .AddSingleton(typeof(AggregateEventDispatcherService<>))
            .AddSingleton(typeof(ILockService<>), typeof(InMemoryLockService<>))

            .Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            .Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            .AddSingleton(typeof(IAggregateExistsQueryHandler<,>), typeof(AggregateExistsQueryHandler<,>))

            .Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            // Add command decorators: should be registered after the command decoratees to wrap them.
            .Decorate(typeof(IAggregateCommandHandler<,>), typeof(AggregateCommandEventDispatcher<,>));
         
    public static IServiceCollection AddInMemoryJsonRepository(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(typeof(IRepository<>), typeof(InMemoryJsonRepository<>))
            .AddSingleton(typeof(AggregateJsonSerializer<>));
}
