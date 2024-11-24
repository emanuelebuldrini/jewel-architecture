using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto
{
    public record GroupEditDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
