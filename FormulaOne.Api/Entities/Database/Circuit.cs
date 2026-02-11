using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Api.Entities.Database {
    public class Circuit {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int RoundNumber { get; set; }
    }
}
