using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Decorators;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Concurrency;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Messaging;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Persistence;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Shared;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddSmartCharging(this IServiceCollection serviceCollection) =>
         serviceCollection
            .AddSingleton<IEventDispatcher, DIEventDispatcher>()
            .AddSingleton(typeof(AggregateEventDispatcherService<>))
            .AddSingleton(typeof(ILockService<>), typeof(InMemoryLockService<>))

            .Scan(scan => scan.FromAssemblyOf<IEventHandler<ChargeStationCreated>>()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            .AddSingleton<GroupCapacityValidatorService>()

            .AddSingleton<GroupService>()
            .AddSingleton<ChargeStationService>()
            .AddSingleton<ChargeStationConnectorService>()

            .Scan(scan => scan.FromAssemblyOf<IQueryHandler<GroupConnectorQuery, GroupConnectorResult>>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            .AddSingleton(typeof(IAggregateExistsQueryHandler<,>), typeof(AggregateExistsQueryHandler<,>))

            .Scan(scan => scan.FromAssemblyOf<ICommandHandler<ChangeGroupCommand>>()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime())

            // Add command decorators: should be registered after the command decoratees to wrap them.
            .Decorate(typeof(IAggregateCommandHandler<,>), typeof(AggregateCommandEventDispatcher<,>))

            // Use cases can be decorated implementing IUseCase<TInput,TOutput>.
            .AddSingleton<CreateChargeStationCase>()
            .AddSingleton<CreateConnectorCase>()
            .AddSingleton<ChangeChargeStationGroupCase>()
            .AddSingleton<RemoveChargeStationCase>()
            .AddSingleton<UpdateGroupCapacityCase>()
            .AddSingleton<RemoveGroupCase>()
            .AddSingleton<UpdateConnectorMaxCurrentCase>();

    public static IServiceCollection AddInMemoryJsonRepository(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton(typeof(IRepository<>), typeof(InMemoryJsonRepository<>))
            .AddSingleton(typeof(AggregateJsonSerializer<>));
}
