using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Api.Entities.Database {
    public class Driver {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public int Number { get; set; }
        [MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int Points { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;
    }
}
