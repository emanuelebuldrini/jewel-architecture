using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using JewelArchitecture.Examples.SmartCharging.WebApi.Groups;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Groups.UnitTests
{
    public class GroupControllerTest : DiTestBase
    {
        [Fact]
        public async Task PostAsync_Success()
        {
            // Arrange
            var groupRepoMock = new RepositoryMock<GroupAggregate>();
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();
            var groupName = "Group 1";
            var capacityAmps = 300;
            var newGroup = new GroupCreateDto
            {
                Name = groupName,
                CapacityAmps = capacityAmps
            };

            // Act
            var response = await controller.PostAsync(newGroup);

            // Assert
            response.ShouldBeOfType<CreatedAtActionResult>();
            var result = response as CreatedAtActionResult;
            result!.Value.ShouldBeOfType<Guid>();
            var resultGroupId = (Guid)result.Value;

            groupRepoMock.Aggregates.Count.ShouldBe(1);
            var repoGroup = groupRepoMock.Aggregates.First();
            repoGroup.Id.ShouldBe(resultGroupId);
            repoGroup.Name.ShouldBe(groupName);
            repoGroup.Capacity.Value.ShouldBe(capacityAmps);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            // Arrange                       
            var groupName = "Group 1";
            var capacityAmps = 300;
            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };
            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.GetAsync(group1.Id);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            okResult!.Value.ShouldBeOfType<GroupAggregate>();
            var resultGroup = okResult!.Value as GroupAggregate;
            resultGroup.ShouldBe(resultGroup);
        }

        [Fact]
        public async Task GetAsync_NotFound()
        {
            // Arrange                       
            var groupName = "Group 1";
            var capacityAmps = 300;
            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };
            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.GetAsync(Guid.NewGuid());

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PutCapacityAsync_Success()
        {
            // Arrange                      
            var groupName = "Group 1";
            var capacityAmps = 300;
            var modifiedCapacityAmps = capacityAmps + 150;

            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.PutCapacityAsync(group1.Id, new GroupUpdateCapacityDto
            {
                CapacityAmps = modifiedCapacityAmps
            });

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(2);
            var repoGroup1 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group1.Id));
            repoGroup1.ShouldNotBeNull();
            // The first group has a change on the capacity.
            repoGroup1.Id.ShouldBe(group1.Id);
            repoGroup1.Name.ShouldBe(groupName);
            repoGroup1.Capacity.Value.ShouldBe(modifiedCapacityAmps);
            // The second group should be unaltered.
            var repoGroup2 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group2.Id));
            repoGroup2.ShouldNotBeNull();
            repoGroup2.ShouldBe(group2);
        }

        [Fact]
        public async Task PutCapacityAsync_NotFound()
        {
            // Arrange                      
            var groupName = "Group 1";
            var capacityAmps = 300;
            var modifiedCapacityAmps = capacityAmps + 150;

            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.PutCapacityAsync(Guid.NewGuid(), new GroupUpdateCapacityDto
            {
                CapacityAmps = modifiedCapacityAmps
            });

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PutAsync_Success()
        {
            // Arrange                      
            var groupName = "Group 1";
            var modifiedGroupName = $"{groupName} (Administrators)";
            var capacityAmps = 300;

            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.PutAsync(group1.Id, new GroupEditDto
            {
                Name = modifiedGroupName
            });

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(2);
            var repoGroup1 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group1.Id));
            repoGroup1.ShouldNotBeNull();
            // The first group has a change on the name.
            repoGroup1.Id.ShouldBe(group1.Id);
            repoGroup1.Name.ShouldBe(modifiedGroupName);
            repoGroup1.Capacity.Value.ShouldBe(capacityAmps);
            // The second group should be unaltered.
            var repoGroup2 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group2.Id));
            repoGroup2.ShouldNotBeNull();
            repoGroup2.ShouldBe(group2);
        }

        [Fact]
        public async Task PutAsync_NotFound()
        {
            // Arrange                      
            var groupName = "Group 1";
            var modifiedGroupName = $"{groupName} (Administrators)";
            var capacityAmps = 300;

            var group1 = new GroupAggregate
            {
                Name = groupName,
                Capacity = new AmpereUnit(capacityAmps)
            };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600) };

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group2, group1]);
            InitScenario(groupRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.PutAsync(Guid.NewGuid(), new GroupEditDto
            {
                Name = modifiedGroupName
            });

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(2);
            var repoGroup1 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group1.Id));
            repoGroup1.ShouldNotBeNull();
            // The groups should be unaltered.
            repoGroup1.ShouldBe(group1);
            var repoGroup2 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id.Equals(group2.Id));
            repoGroup2.ShouldNotBeNull();
            repoGroup2.ShouldBe(group2);
        }

        [Fact]
        public async Task PutCapacityAsync_InvalidValue()
        {
            // Arrange
            var groupCapacity = new AmpereUnit(600);
            var connectorNumber1 = 1; var maxCurrentAmps1 = 500;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = groupCapacity };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
                connectorNumber1, maxCurrentAmps1, group1);
            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.PutCapacityAsync(group1.Id, new GroupUpdateCapacityDto
            {
                CapacityAmps = 450
            });

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            chargeStationRepoMock.Aggregates.Count.ShouldBe(1);
            var repoGroup1 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id == group1.Id);
            // Check that the capacity was not modified.
            repoGroup1!.Capacity.ShouldBe(groupCapacity);
        }

        [Fact]
        public async Task DeleteAsync_CascadeSuccess()
        {
            // Arrange                               
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300), ChargeStations = [] };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600), ChargeStations = [] };
            var connectorNumber1 = 1; var maxCurrentAmps1 = 50;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 60;
            var connectorNumber3 = 5; var maxCurrentAmps3 = 30;
            var chargeStation1 = AggregateFactory.CreateChargeStationWithThreeConnectors(
                            connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, connectorNumber3, maxCurrentAmps3, group1);
            var chargeStation2 = AggregateFactory.CreateChargeStationWithOneConnector(
                            connectorNumber1, maxCurrentAmps1, group1);
            var chargeStation3 = AggregateFactory.CreateChargeStationWithOneConnector(
                            connectorNumber1, maxCurrentAmps1, group2);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1, group2]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1, chargeStation2, chargeStation3]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.DeleteAsync(group1.Id);

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            // Check that the Group 1 was removed and Group 2 is still there.
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            var repoGroup2 = groupRepoMock.Aggregates.SingleOrDefault(s => s.Id == group2.Id);
            repoGroup2.ShouldNotBeNull();
            group2.ShouldBe(repoGroup2);
            // Check that all the associated charging stations to Group 1 were removed.
            chargeStationRepoMock.Aggregates.Count.ShouldBe(1);
            chargeStationRepoMock.Aggregates.ShouldAllBe(c => c.Group.Id != group1.Id);
        }

        [Fact]
        public async Task DeleteAsync_NotFound()
        {
            // Arrange                               
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300), ChargeStations = [] };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(600), ChargeStations = [] };
            var connectorNumber1 = 1; var maxCurrentAmps1 = 50;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 60;
            var connectorNumber3 = 5; var maxCurrentAmps3 = 30;
            var chargeStation1 = AggregateFactory.CreateChargeStationWithThreeConnectors(
                            connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, connectorNumber3, maxCurrentAmps3, group1);
            var chargeStation2 = AggregateFactory.CreateChargeStationWithOneConnector(
                            connectorNumber1, maxCurrentAmps1, group1);
            var chargeStation3 = AggregateFactory.CreateChargeStationWithOneConnector(
                            connectorNumber1, maxCurrentAmps1, group2);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1, group2]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1, chargeStation2, chargeStation3]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<GroupController>();

            // Act
            var response = await controller.DeleteAsync(Guid.NewGuid());

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }
    }
}