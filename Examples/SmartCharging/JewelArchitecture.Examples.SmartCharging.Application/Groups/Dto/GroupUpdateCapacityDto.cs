using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto
{
    public record GroupUpdateCapacityDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public required int? CapacityAmps { get; set; }
    }
}
