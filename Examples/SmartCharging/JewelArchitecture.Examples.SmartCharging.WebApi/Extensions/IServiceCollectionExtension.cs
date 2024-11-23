using JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers.Decorators;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.EventHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Groups.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Core.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.EventDispatchers;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Locks;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Repositories;
using JewelArchitecture.Examples.SmartCharging.Infrastructure.Serializers;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Extensions;

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
