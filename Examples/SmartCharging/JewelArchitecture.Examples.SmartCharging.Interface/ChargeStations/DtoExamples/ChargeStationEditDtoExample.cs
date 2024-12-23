﻿using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.Interface.ChargeStations.DtoExamples;

public class ChargeStationEditDtoExample : IExamplesProvider<ChargeStationEditDto>
{
    public ChargeStationEditDto GetExamples() => new()
    {
        Name = "New Charge Station Name"
    };
}
