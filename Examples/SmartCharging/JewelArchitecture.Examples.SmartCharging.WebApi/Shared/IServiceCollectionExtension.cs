using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.EventHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Decorators;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents;
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

            // Handlers registration can be automatized using an assembly scanner, e.g. Scrutor.
            .AddSingleton<IEventHandler<ChargeStationCascadeRemoval>, ChargeStationCascadeRemovalHandler>()
            .AddSingleton<IEventHandler<ChargeStationCreated>, ChargeStationCreatedHandler>()
            .AddSingleton<IEventHandler<ChargeStationRemoved>, ChargeStationRemovedHandler>()
            .AddSingleton<IEventHandler<ChargeStationGroupChanged>, ChargeStationGroupChangedHandler>()

            .AddSingleton<GroupCapacityValidatorService>()

            .AddSingleton<GroupService>()
            .AddSingleton<ChargeStationService>()
            .AddSingleton<ChargeStationConnectorService>()

            // Add queries
            .AddSingleton<IQueryHandler<GroupByIdQuery, GroupAggregate>, GroupByIdQueryHandler>()
            .AddSingleton<IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate>, ChargeStationByIdQueryHandler>()
            .AddSingleton<IQueryHandler<GroupConnectorQuery, GroupConnectorResult>, GroupConnectorQueryHandler>()
            .AddSingleton<IQueryHandler<GroupChargeStationConnectorQuery, GroupChargeStationConnectorResult>, GroupChargeStationConnectorQueryHandler>()
            .AddSingleton<IQueryHandler<ChangeGroupChargeStationConnectorQuery, GroupChargeStationConnectorResult>, ChangeGroupChargeStationConnectorQueryHandler>()
            .AddSingleton(typeof(IAggregateExistsQueryHandler<,>), typeof(AggregateExistsQueryHandler<,>))

            // Add commands
            .AddSingleton<ICommandHandler<ChangeGroupCommand>, ChangeGroupCommandHandler>()
            .AddSingleton<ICommandHandler<UpdateMaxCurrentCommand>, UpdateMaxCurrentCommandHandler>()
            .AddSingleton<ICommandHandler<UpdateGroupCapacityCommand>, UpdateGroupCapacityCommandHandler>()
            .AddSingleton<ICommandHandler<AddChargeStationCommand>, AddChargeStationCommandHandler>()
            .AddSingleton<ICommandHandler<AddGroupCommand>, AddGroupCommandHandler>()
            .AddSingleton<ICommandHandler<AddConnectorCommand>, AddConnectorCommandHandler>()
            .AddSingleton<ICommandHandler<RemoveChargeStationCommand>, RemoveChargeStationCommandHandler>()
            .AddSingleton<ICommandHandler<RemoveGroupCommand>, RemoveGroupCommandHandler>()
            .AddSingleton<ICommandHandler<RemoveConnectorCommand>, RemoveConnectorCommandHandler>()

            // Add command decorators: should be registered after the command decoratees to wrap them.
            .AddSingleton(typeof(IAggregateCommandHandler<,>), typeof(AggregateCommandEventDispatcher<,>))

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
