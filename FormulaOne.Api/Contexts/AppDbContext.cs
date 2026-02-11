using FormulaOne.Api.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Api.Contexts {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Circuit> Circuits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.Number)
                .IsUnique();

            modelBuilder.Entity<Driver>()
                .ToTable(t => t.HasCheckConstraint("CK_Driver_Points", "Points >= 0"));

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Team)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Teams
            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Red Bull Racing", TeamPrincipal = "Christian Horner", Country = "Austria", City = "Milton Keynes" },
                new Team { Id = 2, Name = "Ferrari", TeamPrincipal = "Frederic Vasseur", Country = "Italy", City = "Maranello" },
                new Team { Id = 3, Name = "Mercedes", TeamPrincipal = "Toto Wolff", Country = "Germany", City = "Brackley" },
                new Team { Id = 4, Name = "McLaren", TeamPrincipal = "Andrea Stella", Country = "United Kingdom", City = "Woking" },
                new Team { Id = 5, Name = "Aston Martin", TeamPrincipal = "Mike Krack", Country = "United Kingdom", City = "Silverstone" },
                new Team { Id = 6, Name = "Alpine", TeamPrincipal = "Oliver Oakes", Country = "France", City = "Enstone" },
                new Team { Id = 7, Name = "Williams", TeamPrincipal = "James Vowles", Country = "United Kingdom", City = "Grove" },
                new Team { Id = 8, Name = "Haas", TeamPrincipal = "Ayao Komatsu", Country = "United States", City = "Kannapolis" },
                new Team { Id = 9, Name = "RB", TeamPrincipal = "Laurent Mekies", Country = "Italy", City = "Faenza" },
                new Team { Id = 10, Name = "Kick Sauber", TeamPrincipal = "Alessandro Alunni Bravi", Country = "Switzerland", City = "Hinwil" }
            );

            // Seed Drivers
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, FullName = "Max Verstappen", Number = 1, Nationality = "Dutch", IsActive = true, Points = 437, TeamId = 1 },
                new Driver { Id = 2, FullName = "Sergio Perez", Number = 11, Nationality = "Mexican", IsActive = true, Points = 152, TeamId = 1 },
                new Driver { Id = 3, FullName = "Charles Leclerc", Number = 16, Nationality = "Monegasque", IsActive = true, Points = 356, TeamId = 2 },
                new Driver { Id = 4, FullName = "Carlos Sainz", Number = 55, Nationality = "Spanish", IsActive = true, Points = 290, TeamId = 2 },
                new Driver { Id = 5, FullName = "Lewis Hamilton", Number = 44, Nationality = "British", IsActive = true, Points = 234, TeamId = 3 },
                new Driver { Id = 6, FullName = "George Russell", Number = 63, Nationality = "British", IsActive = true, Points = 217, TeamId = 3 },
                new Driver { Id = 7, FullName = "Lando Norris", Number = 4, Nationality = "British", IsActive = true, Points = 374, TeamId = 4 },
                new Driver { Id = 8, FullName = "Oscar Piastri", Number = 81, Nationality = "Australian", IsActive = true, Points = 292, TeamId = 4 },
                new Driver { Id = 9, FullName = "Fernando Alonso", Number = 14, Nationality = "Spanish", IsActive = true, Points = 70, TeamId = 5 },
                new Driver { Id = 10, FullName = "Lance Stroll", Number = 18, Nationality = "Canadian", IsActive = true, Points = 24, TeamId = 5 },
                new Driver { Id = 11, FullName = "Pierre Gasly", Number = 10, Nationality = "French", IsActive = true, Points = 42, TeamId = 6 },
                new Driver { Id = 12, FullName = "Esteban Ocon", Number = 31, Nationality = "French", IsActive = true, Points = 58, TeamId = 6 },
                new Driver { Id = 13, FullName = "Alexander Albon", Number = 23, Nationality = "Thai", IsActive = true, Points = 12, TeamId = 7 },
                new Driver { Id = 14, FullName = "Logan Sargeant", Number = 2, Nationality = "American", IsActive = true, Points = 6, TeamId = 7 },
                new Driver { Id = 15, FullName = "Kevin Magnussen", Number = 20, Nationality = "Danish", IsActive = true, Points = 16, TeamId = 8 },
                new Driver { Id = 16, FullName = "Nico Hulkenberg", Number = 27, Nationality = "German", IsActive = true, Points = 31, TeamId = 8 },
                new Driver { Id = 17, FullName = "Daniel Ricciardo", Number = 3, Nationality = "Australian", IsActive = true, Points = 12, TeamId = 9 },
                new Driver { Id = 18, FullName = "Yuki Tsunoda", Number = 22, Nationality = "Japanese", IsActive = true, Points = 22, TeamId = 9 },
                new Driver { Id = 19, FullName = "Valtteri Bottas", Number = 77, Nationality = "Finnish", IsActive = true, Points = 0, TeamId = 10 },
                new Driver { Id = 20, FullName = "Zhou Guanyu", Number = 24, Nationality = "Chinese", IsActive = true, Points = 6, TeamId = 10 }
            );

            // Seed Circuits
            modelBuilder.Entity<Circuit>().HasData(
                new Circuit { Id = 1, Name = "Bahrain International Circuit", Country = "Bahrain", City = "Sakhir", Lat = 26.0325, Lng = 50.5106, RoundNumber = 1 },
                new Circuit { Id = 2, Name = "Jeddah Corniche Circuit", Country = "Saudi Arabia", City = "Jeddah", Lat = 21.6319, Lng = 39.1044, RoundNumber = 2 },
                new Circuit { Id = 3, Name = "Albert Park Circuit", Country = "Australia", City = "Melbourne", Lat = -37.8497, Lng = 144.9680, RoundNumber = 3 },
                new Circuit { Id = 4, Name = "Suzuka International Racing Course", Country = "Japan", City = "Suzuka", Lat = 34.8431, Lng = 136.5407, RoundNumber = 4 },
                new Circuit { Id = 5, Name = "Shanghai International Circuit", Country = "China", City = "Shanghai", Lat = 31.3389, Lng = 121.2197, RoundNumber = 5 },
                new Circuit { Id = 6, Name = "Miami International Autodrome", Country = "United States", City = "Miami", Lat = 25.9581, Lng = -80.2389, RoundNumber = 6 },
                new Circuit { Id = 7, Name = "Autodromo Enzo e Dino Ferrari", Country = "Italy", City = "Imola", Lat = 44.3439, Lng = 11.7167, RoundNumber = 7 },
                new Circuit { Id = 8, Name = "Circuit de Monaco", Country = "Monaco", City = "Monte Carlo", Lat = 43.7347, Lng = 7.4206, RoundNumber = 8 },
                new Circuit { Id = 9, Name = "Circuit de Barcelona-Catalunya", Country = "Spain", City = "Barcelona", Lat = 41.5700, Lng = 2.2611, RoundNumber = 9 },
                new Circuit { Id = 10, Name = "Circuit Gilles Villeneuve", Country = "Canada", City = "Montreal", Lat = 45.5000, Lng = -73.5228, RoundNumber = 10 },
                new Circuit { Id = 11, Name = "Red Bull Ring", Country = "Austria", City = "Spielberg", Lat = 47.2197, Lng = 14.7647, RoundNumber = 11 },
                new Circuit { Id = 12, Name = "Silverstone Circuit", Country = "United Kingdom", City = "Silverstone", Lat = 52.0786, Lng = -1.0169, RoundNumber = 12 },
                new Circuit { Id = 13, Name = "Hungaroring", Country = "Hungary", City = "Budapest", Lat = 47.5789, Lng = 19.2486, RoundNumber = 13 },
                new Circuit { Id = 14, Name = "Circuit de Spa-Francorchamps", Country = "Belgium", City = "Spa", Lat = 50.4372, Lng = 5.9714, RoundNumber = 14 },
                new Circuit { Id = 15, Name = "Circuit Zandvoort", Country = "Netherlands", City = "Zandvoort", Lat = 52.3888, Lng = 4.5409, RoundNumber = 15 },
                new Circuit { Id = 16, Name = "Autodromo Nazionale Monza", Country = "Italy", City = "Monza", Lat = 45.6156, Lng = 9.2811, RoundNumber = 16 },
                new Circuit { Id = 17, Name = "Baku City Circuit", Country = "Azerbaijan", City = "Baku", Lat = 40.3725, Lng = 49.8533, RoundNumber = 17 },
                new Circuit { Id = 18, Name = "Marina Bay Street Circuit", Country = "Singapore", City = "Singapore", Lat = 1.2914, Lng = 103.8639, RoundNumber = 18 },
                new Circuit { Id = 19, Name = "Circuit of the Americas", Country = "United States", City = "Austin", Lat = 30.1328, Lng = -97.6411, RoundNumber = 19 },
                new Circuit { Id = 20, Name = "Autodromo Hermanos Rodriguez", Country = "Mexico", City = "Mexico City", Lat = 19.4042, Lng = -99.0907, RoundNumber = 20 },
                new Circuit { Id = 21, Name = "Autodromo Jose Carlos Pace", Country = "Brazil", City = "São Paulo", Lat = -23.7036, Lng = -46.6997, RoundNumber = 21 },
                new Circuit { Id = 22, Name = "Las Vegas Street Circuit", Country = "United States", City = "Las Vegas", Lat = 36.1147, Lng = -115.1728, RoundNumber = 22 },
                new Circuit { Id = 23, Name = "Losail International Circuit", Country = "Qatar", City = "Lusail", Lat = 25.4900, Lng = 51.4542, RoundNumber = 23 },
                new Circuit { Id = 24, Name = "Yas Marina Circuit", Country = "UAE", City = "Abu Dhabi", Lat = 24.4672, Lng = 54.6031, RoundNumber = 24 }
            );
        }
    }
}
