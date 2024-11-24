using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Examples;

public class ChargeStationEditDtoExample : IExamplesProvider<ChargeStationEditDto>
{
    public ChargeStationEditDto GetExamples() => new()
    {
        Name = "New Charge Station Name"
    };
}
