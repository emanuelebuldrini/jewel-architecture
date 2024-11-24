
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto
{
    public record ChargeStationChangeGroupDto
    {
        [Required]
        public required Guid? GroupId { get; init; }
    }
}
