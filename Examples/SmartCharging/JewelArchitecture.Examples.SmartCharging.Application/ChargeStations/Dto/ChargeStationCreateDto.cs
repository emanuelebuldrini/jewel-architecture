using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto
{
    public record ChargeStationCreateDto : ChargeStationEditDto
    {
        [Required]
        public required Guid? GroupReference { get; init; }

        [Required]
        [MinLength(1)]
        public required ChargeStationConnectorCreateDto[] Connectors { get; init; }
    }
}
