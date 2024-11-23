
using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation
{
    public record ChargeStationChangeGroupDto
    {
        [Required]
        public required Guid? GroupId { get; init; }        
    }
}
