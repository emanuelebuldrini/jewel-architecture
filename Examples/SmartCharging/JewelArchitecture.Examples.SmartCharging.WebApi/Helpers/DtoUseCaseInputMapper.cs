﻿using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.WebApi.Helpers
{
    public static class DtoUseCaseInputMapper
    {
        public static CreateChargeStationInput MapCreateChargeStationDto(ChargeStationCreateDto dto)
        {
            var chargeStationConnectors = new List<(ConnectorId,AmpereUnit)>();
            foreach (var connectorCreateDto in dto.Connectors)
            {
                chargeStationConnectors.Add((new ConnectorId(connectorCreateDto.Id!.Value),
                    new AmpereUnit(connectorCreateDto.MaxCurrentAmps!.Value)));
            }

            return new CreateChargeStationInput(dto.Name,
                new GroupReference(dto.GroupReference!.Value), chargeStationConnectors.AsReadOnly());           
        }
    }
}
