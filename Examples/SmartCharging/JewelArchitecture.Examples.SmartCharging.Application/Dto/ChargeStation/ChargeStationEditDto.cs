
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation
{
    public record ChargeStationEditDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; init; }        
    }
}
