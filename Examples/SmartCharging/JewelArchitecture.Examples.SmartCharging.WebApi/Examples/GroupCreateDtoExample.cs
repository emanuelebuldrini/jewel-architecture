using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Examples;

public class GroupCreateDtoExample : IExamplesProvider<GroupCreateDto>
{
    public GroupCreateDto GetExamples() => new()
    {
        Name = "Group 1",
        CapacityAmps=300
    };
}
