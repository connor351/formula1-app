using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Api.Entities.DTOs {
    public class TeamRequestDto {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string TeamPrincipal { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
    }

    public class TeamResponseDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TeamPrincipal { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int DriverCount { get; set; }
    }
}
