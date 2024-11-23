using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.Group;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using JewelArchitecture.Examples.SmartCharging.WebApi.Controllers;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Factories;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.TestBases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest
{
    public class GroupChargeStationControllerConcurrencyTest : ConcurrencyDITestBase
    {
        [Fact]
        public async Task PutGroupAsync_PutCapacityAsync_Success_BadRequest()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group2capacity = 400;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(group2capacity) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>();
            await slowGroupRepo.AddOrReplaceAsync(group1);
            await slowGroupRepo.AddOrReplaceAsync(group2);
            var slowerChargeStationRepo = InMemoryRepositoryFactory
                .GetSlowWriteInMemoryRepository<ChargeStationAggregate>(SlowerWriteMsDelay);
            await slowerChargeStationRepo.AddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationController>();
            var controller2 = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var newGroupReference = group2.Id;
            var task1 = Task.Run(() =>
                 controller1.PutGroupAsync(chargeStation1.Id, new ChargeStationChangeGroupDto
                 {
                     GroupId = newGroupReference
                 })
            );
            await ShortWaitAsync();
            var task2 = Task.Run(() =>
                 controller2.PutCapacityAsync(group2.Id, new GroupUpdateCapacityDto
                 {
                     CapacityAmps = 150
                 })
            );

            await Task.WhenAll(task1, task2);

            // Assert
            task1.Result.ShouldBeOfType<NoContentResult>();
            task2.Result.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation1 = await slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            repoChargeStation1.Group.Id.ShouldBe(newGroupReference);
            var repoGroup2 = await slowGroupRepo.GetSingleAsync(group2.Id);
            // The capacity reduction should not be allowed after swapping the charging station in group 2.
            repoGroup2.Capacity.Value.ShouldBe(group2capacity);
        }

        [Fact]
        public async Task PutMaxCurrentAsync_PutGroupAsync_Success_BadRequest()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group2capacity = 250;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(270) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(group2capacity) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>();
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            await slowGroupRepo.FastAddOrReplaceAsync(group2);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(SlowerWriteMsDelay);
            await slowerChargeStationRepo.FastAddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationController>();
            var controller2 = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var task1 = Task.Run(() =>
                // This action takes more time than the next one due to the slower repository.
                controller2.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                    new ChargeStationConnectorUpdateMaxCurrentDto
                    {
                        MaxCurrentAmps = 170
                    })
                );
            await ShortWaitAsync();
            var newGroupReference = group2.Id;
            var task2 = Task.Run(() =>
              controller1.PutGroupAsync(chargeStation1.Id, new ChargeStationChangeGroupDto
              {
                  GroupId = newGroupReference
              })
            );

            await Task.WhenAll(task1, task2);

            // Assert
            task1.Result.ShouldBeOfType<NoContentResult>();
            task2.Result.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation1 = await slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            repoChargeStation1.Group.Id.ShouldBe(group1.Id);
            var repoGroup2 = await slowGroupRepo.GetSingleAsync(group2.Id);
            // The change of group should not be allowed after increasing the max current of connector 1.
            repoGroup2.Capacity.Value.ShouldBe(group2capacity);
        }

        [Fact]
        public async Task Connector_CreateAsync_PutGroupAsync_Success_BadRequest()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var connectorNumber3 = 3; var maxCurrentAmps3 = 10;
            var group1capacity = 270;
            var group2capacity = 160;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(group1capacity) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(group2capacity) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>(2,
                // Set up a coordinator to control start write time.          
                startWriteSignal: Synchronizer);
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            await slowGroupRepo.FastAddOrReplaceAsync(group2);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(SlowerWriteMsDelay,
                startWriteSignal: Synchronizer);
            await slowerChargeStationRepo.FastAddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationController>();
            var controller2 = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            // This action takes more time than the next one due to the slower repository.
            var task1 = controller2.PostAsync(chargeStation1.Id, new ChargeStationConnectorCreateDto
            {
                Id = connectorNumber3,
                MaxCurrentAmps = maxCurrentAmps3
            });
            await ShortWaitAsync();
            var newGroupReference = group2.Id;
            var task2 = controller1.PutGroupAsync(chargeStation1.Id, new ChargeStationChangeGroupDto
            {
                GroupId = newGroupReference
            });

            await SimulateConcurrency();

            await Task.WhenAll(task1, task2);

            // Assert
            task1.Result.ShouldBeOfType<CreatedAtActionResult>();
            task2.Result.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation1 = await slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            // The change of group should not be allowed after adding an additional connector.
            repoChargeStation1.Group.Id.ShouldBe(group1.Id);
        }

        [Fact]
        public async Task ChargeStation_CreateAsync_PutCapacityAsync_Success_BadRequest()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 170;
            var group1capacity = 270;
            var editedGroup1capacity = 150;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(group1capacity) };

            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>(2,
                // Set up a coordinator to control start write time.          
                startWriteSignal: Synchronizer);
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(SlowerWriteMsDelay,
                startWriteSignal: Synchronizer);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationController>();
            var controller2 = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            // This action takes more time than the next one due to the slower repository.
            var task1 = Task.Run(() => controller1.PostAsync(new ChargeStationCreateDto
            {
                Name = "Charge Station 1",
                GroupReference = group1.Id,
                Connectors = [new ChargeStationConnectorCreateDto { Id = connectorNumber1,
                    MaxCurrentAmps = maxCurrentAmps1 }]
            }));
            await ShortWaitAsync(); // Make sure that the second task starts a bit later.
            var task2 = Task.Run(() => controller2.PutCapacityAsync(group1.Id, new GroupUpdateCapacityDto
            {
                CapacityAmps = editedGroup1capacity
            }));

            await SimulateConcurrency();

            await Task.WhenAll(task1, task2);

            // Assert
            task1.Result.ShouldBeOfType<CreatedAtActionResult>();
            task2.Result.ShouldBeOfType<BadRequestObjectResult>();
            var chargeStationId = ((CreatedAtActionResult)task1.Result).Value;
            chargeStationId.ShouldNotBeNull();
            var repoChargeStation1 = await slowerChargeStationRepo.GetSingleAsync((Guid)chargeStationId);
            repoChargeStation1.ShouldNotBeNull();
            // The change of group should not be allowed after adding an additional connector.
            repoChargeStation1.Group.Id.ShouldBe(group1.Id);
        }

        [Fact]
        public async Task Connector_PutMaxCurrentAsync_Group_DeleteAync_Success_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(270) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
                connectorNumber1, maxCurrentAmps1, group1);

            // Set up a coordinator to control start write time.
            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>(
                startWriteSignal: Synchronizer);
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(
                addOrReplaceMsDelay: 350, // The add is much slower than the remove.
                startWriteSignal: Synchronizer);
            await slowerChargeStationRepo.FastAddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();
            var controller2 = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            // The update takes long time due to the slow repo.
            // There is the risk that it writes the record after that has been deleted by the second call.
            var task1 = Task.Run(() => controller1.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                    new ChargeStationConnectorUpdateMaxCurrentDto
                    {
                        MaxCurrentAmps = 170
                    }));
            await ShortWaitAsync();
            var task2 = Task.Run(() => controller2.DeleteAsync(group1.Id));

            await SimulateConcurrency();

            var results = await Task.WhenAll(task1, task2);

            // Assert            
            results[0].ShouldBeOfType<NoContentResult>();
            results[1].ShouldBeOfType<NoContentResult>();
            // The connector is updated and then the group is deleted.
            // Therefore also the related charge station and connectors should be deleted.
            slowGroupRepo.GetSingleAsync(group1.Id).ShouldThrow<KeyNotFoundException>();
            slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id).ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public async Task Connector_PutMaxCurrentAsync_ChargeStation_DeleteAync_Success_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(270) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
                connectorNumber1, maxCurrentAmps1, group1);

            // Set up a coordinator to control start write time.      
            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>(
                startWriteSignal: Synchronizer);
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(
                removeMsDelay: 2,
                addOrReplaceMsDelay: 300, // The add is much slower than the remove.
                startWriteSignal: Synchronizer);
            await slowerChargeStationRepo.FastAddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller1 = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();
            var controller2 = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            // The update takes long time due to the slow repo.
            // There is the risk that it writes the record after that has been deleted by the second call.
            var task1 = Task.Run(() => controller1.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                    new ChargeStationConnectorUpdateMaxCurrentDto
                    {
                        MaxCurrentAmps = 170
                    }));            
            await ShortWaitAsync();            
            var task2 = controller2.DeleteAsync(chargeStation1.Id);

            await SimulateConcurrency();

            var results = await Task.WhenAll(task1, task2);

            // Assert            
            results[0].ShouldBeOfType<NoContentResult>();
            results[1].ShouldBeOfType<NoContentResult>();
            await slowGroupRepo.GetSingleAsync(group1.Id).ShouldNotBeNull();
            // The connector is updated and then the charge station is deleted.
            slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id).ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public async Task Connector_PutMaxCurrentAsync_Connector_DeleteAync_Success_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 170;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(370) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                 connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            // Set up a coordinator to control start write time.      
            var slowGroupRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<GroupAggregate>(
                startWriteSignal: Synchronizer);
            await slowGroupRepo.FastAddOrReplaceAsync(group1);
            var slowerChargeStationRepo = InMemoryRepositoryFactory.GetSlowWriteInMemoryRepository<ChargeStationAggregate>(
                removeMsDelay: 1,
                addOrReplaceMsDelay: 500, // The add is much slower than the remove.
                startWriteSignal: Synchronizer);
            await slowerChargeStationRepo.FastAddOrReplaceAsync(chargeStation1);

            InitScenario(slowGroupRepo, slowerChargeStationRepo);

            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            // The update takes long time due to the slow repo.
            // There is the risk that it writes the record after that has been deleted by the second call.
            var task1 = Task.Run(() => controller.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                    new ChargeStationConnectorUpdateMaxCurrentDto
                    {
                        MaxCurrentAmps = 170
                    }));
            await ShortWaitAsync();
            var task2 = Task.Run(() => controller.DeleteAsync(chargeStation1.Id, connectorNumber1));

            await SimulateConcurrency();

            var results = await Task.WhenAll(task1, task2);

            // Assert            
            results[0].ShouldBeOfType<NoContentResult>();
            results[1].ShouldBeOfType<NoContentResult>();
            await slowGroupRepo.GetSingleAsync(group1.Id).ShouldNotBeNull();
            var chargeStation = await slowerChargeStationRepo.GetSingleAsync(chargeStation1.Id);
            // The connector is updated and then the connector is deleted.
            chargeStation.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber1).ShouldBeNull();
        }
    }
}
