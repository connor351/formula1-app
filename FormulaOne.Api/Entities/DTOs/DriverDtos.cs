using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Api.Entities.DTOs {
    public class DriverResponseDto {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Points { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
    }

    public class DriverRequestDto {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [Range(1,99)]
        public int Number { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
        public int Points { get; set; }
        [Required]
        public int TeamId { get; set; }
    }
}
