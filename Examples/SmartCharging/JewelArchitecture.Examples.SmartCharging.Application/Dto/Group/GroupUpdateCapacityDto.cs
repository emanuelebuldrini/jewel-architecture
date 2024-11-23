using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.Group
{
    public record GroupUpdateCapacityDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public required int? CapacityAmps { get; set; }
    }
}
