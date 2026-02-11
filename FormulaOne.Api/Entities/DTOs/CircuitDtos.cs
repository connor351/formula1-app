namespace FormulaOne.Api.Entities.DTOs {
    public class CircuitResponseDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int RoundNumber { get; set; }
    }
}
