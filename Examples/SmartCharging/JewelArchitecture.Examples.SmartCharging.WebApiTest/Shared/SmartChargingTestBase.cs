using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Test;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared;

public class SmartChargingTestBase : DiTestBase
{
    protected override ServiceCollection GetServiceCollection()
        => ServiceCollectionFactory.GetSmartCharging();

    protected void InitScenario(IRepository<GroupAggregate> groupRepoMock,
         IRepository<ChargeStationAggregate>? chargeStationRepoMock = null)
    {
        SmartChargingScenario.InitScenario(_serviceCollection, groupRepoMock, chargeStationRepoMock);
        
        BuildServiceProvider();
    }
}
