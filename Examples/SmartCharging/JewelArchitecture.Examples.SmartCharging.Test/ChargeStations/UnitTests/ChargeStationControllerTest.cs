using JewelArchitecture.Core.Test;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Examples.SmartCharging.Interface.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Test.Shared;
using JewelArchitecture.Examples.SmartCharging.Test.Shared.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace JewelArchitecture.Examples.SmartCharging.Test.ChargeStations.UnitTests
{
    public class ChargeStationControllerTest : SmartChargingTestBase
    {
        [Fact]
        public async Task PostAsync_Success()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var name = "Charge station 1"; var connectorNumber = 1; var maxCurrentAmps = 30;
            var newChargeStation = new ChargeStationCreateDto
            {
                Name = name,
                GroupReference = group1.Id,
                Connectors = [new ChargeStationConnectorCreateDto { Id = connectorNumber,
                    MaxCurrentAmps = maxCurrentAmps }]
            };

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>();
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.PostAsync(newChargeStation);

            // Assert
            response.ShouldBeOfType<CreatedAtActionResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            var repoGroup = groupRepoMock.Aggregates.Single();
            chargeStationRepoMock.Aggregates.Count.ShouldBe(1);
            var repoChargeStation = chargeStationRepoMock.Aggregates.Single();
            repoGroup.ChargeStations.Count.ShouldBe(1);
            repoGroup.ChargeStations.Single().Id.ShouldBe(repoChargeStation.Id);
            repoChargeStation.Name.ShouldBe(name);
            repoChargeStation.Group.Id.ShouldBe(group1.Id);
            repoChargeStation.Connectors.Count.ShouldBe(1);
            var repoConnector = repoChargeStation.Connectors.Single();
            repoConnector.Id.Value.ShouldBe(connectorNumber);
            repoConnector.MaxCurrent.Value.ShouldBe(maxCurrentAmps);
        }

        [Fact]
        public async Task PostAsync_InvalidMaxCurrent()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var name = "Charge station 1"; var connectorNumber = 1; var maxCurrentAmps = 330;
            var newChargeStation = new ChargeStationCreateDto
            {
                Name = name,
                GroupReference = group1.Id,
                Connectors = [new ChargeStationConnectorCreateDto { Id = connectorNumber,
                    MaxCurrentAmps = maxCurrentAmps }]
            };

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>();
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.PostAsync(newChargeStation);

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            // Check that the charge station was not created due to invalid connector max current.
            chargeStationRepoMock.Aggregates.Count.ShouldBe(0);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            // Arrange                                  
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.GetAsync(chargeStation1.Id);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            okResult!.Value.ShouldBeOfType<ChargeStationAggregate>();
            var resultChargeStation = okResult!.Value as ChargeStationAggregate;
            resultChargeStation.ShouldBe(chargeStation1);
        }

        [Fact]
        public async Task GetAsync_NotFound()
        {
            // Arrange                                  
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.GetAsync(Guid.NewGuid());

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PutGroupAsync_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(400) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1, group2]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var newGroupReference = group2.Id;
            var response = await controller.PutGroupAsync(chargeStation1.Id, new ChargeStationChangeGroupDto
            {
                GroupId = newGroupReference
            });

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            repoChargeStation1.Group.Id.ShouldBe(newGroupReference);
        }

        [Fact]
        public async Task PutGroupAsync_NotFound()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(400) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1, group2]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var newGroupReference = group2.Id;
            var response = await controller.PutGroupAsync(Guid.NewGuid(), new ChargeStationChangeGroupDto
            {
                GroupId = newGroupReference
            });

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PutGroupAsync_InvalidMaxCurrent()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var group2 = new GroupAggregate { Name = "Group 2", Capacity = new AmpereUnit(100) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1, group2]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var newGroupReference = group2.Id;
            var response = await controller.PutGroupAsync(chargeStation1.Id, new ChargeStationChangeGroupDto
            {
                GroupId = newGroupReference
            });

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            // Check that the group reference was not modified due to invalid connector max current.
            repoChargeStation1.Group.Id.ShouldBe(group1.Id);
        }

        [Fact]
        public async Task PutAsync_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();
            // Act
            var newName = "123y";
            var response = await controller.PutAsync(chargeStation1.Id, new ChargeStationEditDto
            {
                Name = newName
            });

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            repoChargeStation1.Name.ShouldBe(newName);
        }

        [Fact]
        public async Task PutAsync_NotFound()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();
            // Act
            var newName = "123y";
            var response = await controller.PutAsync(Guid.NewGuid(), new ChargeStationEditDto
            {
                Name = newName
            });

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.DeleteAsync(chargeStation1.Id);

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            var groupRepo = groupRepoMock.Aggregates.Single();
            groupRepo.ChargeStations.Count.ShouldBe(0);
            chargeStationRepoMock.Aggregates.Count.ShouldBe(0);
        }

        [Fact]
        public async Task DeleteAsync_NotFound()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate, Guid>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate, Guid>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationController>();

            // Act
            var response = await controller.DeleteAsync(Guid.NewGuid());

            // Assert
            response.ShouldBeOfType<NotFoundResult>();
        }
    }
}