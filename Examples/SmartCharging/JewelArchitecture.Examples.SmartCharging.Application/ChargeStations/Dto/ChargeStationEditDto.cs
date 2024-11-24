
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto
{
    public record ChargeStationEditDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; init; }
    }
}
