using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Shared;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddSmartCharging(this IServiceCollection serviceCollection) =>
         serviceCollection            
            .AddSingleton<GroupCapacityValidatorService>()

            .AddSingleton<GroupService>()
            .AddSingleton<ChargeStationService>()
            .AddSingleton<ChargeStationConnectorService>()
            
            // Use cases can be decorated implementing IUseCase<TInput,TOutput>.
            .AddSingleton<CreateChargeStationCase>()
            .AddSingleton<CreateConnectorCase>()
            .AddSingleton<ChangeChargeStationGroupCase>()
            .AddSingleton<RemoveChargeStationCase>()
            .AddSingleton<UpdateGroupCapacityCase>()
            .AddSingleton<RemoveGroupCase>()
            .AddSingleton<UpdateConnectorMaxCurrentCase>();
}
