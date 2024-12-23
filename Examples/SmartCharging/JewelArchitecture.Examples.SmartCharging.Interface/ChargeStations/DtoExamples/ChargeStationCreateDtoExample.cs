﻿using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace JewelArchitecture.Examples.SmartCharging.Interface.ChargeStations.DtoExamples;

public class ChargeStationCreateDtoExample : IExamplesProvider<ChargeStationCreateDto>
{
    public ChargeStationCreateDto GetExamples() => new()
    {
        Name = "Charge Station 1",
        GroupReference = Guid.NewGuid(),
        Connectors = [
                new ChargeStationConnectorCreateDto
            {
                Id=1,
                MaxCurrentAmps=13
            },
                new ChargeStationConnectorCreateDto
            {
                Id=2,
                MaxCurrentAmps=8
            }
        ]
    };
}
