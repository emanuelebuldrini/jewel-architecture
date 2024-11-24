using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto
{
    public record ChargeStationConnectorCreateDto : ChargeStationConnectorUpdateMaxCurrentDto
    {
        [Required]
        [Range(1, 5)]
        public required int? Id { get; init; }
    }
}