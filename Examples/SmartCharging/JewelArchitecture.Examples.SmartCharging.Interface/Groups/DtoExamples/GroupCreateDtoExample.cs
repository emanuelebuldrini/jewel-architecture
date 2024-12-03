using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.Interface.Groups.DtoExamples;

public class GroupCreateDtoExample : IExamplesProvider<GroupCreateDto>
{
    public GroupCreateDto GetExamples() => new()
    {
        Name = "Group 1",
        CapacityAmps = 300
    };
}
