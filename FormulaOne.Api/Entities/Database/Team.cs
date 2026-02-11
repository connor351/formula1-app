using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Api.Entities.Database {
    public class Team {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string TeamPrincipal { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    }
}
