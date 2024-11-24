using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto
{
    public record ChargeStationConnectorUpdateMaxCurrentDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public required int? MaxCurrentAmps { get; init; }
    }
}