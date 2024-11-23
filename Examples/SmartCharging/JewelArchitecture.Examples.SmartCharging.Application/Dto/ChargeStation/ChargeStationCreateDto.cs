using System.ComponentModel.DataAnnotations;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation
{
    public record ChargeStationCreateDto: ChargeStationEditDto
    {
        [Required]
        public required Guid? GroupReference { get; init; }

        [Required]
        [MinLength(1)]
        public required ChargeStationConnectorCreateDto[] Connectors { get; init; }
    }
}
