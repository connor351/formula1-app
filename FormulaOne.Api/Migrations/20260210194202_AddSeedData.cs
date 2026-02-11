using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormulaOne.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Circuits",
                columns: new[] { "Id", "City", "Country", "Lat", "Lng", "Name", "RoundNumber" },
                values: new object[,]
                {
                    { 1, "Sakhir", "Bahrain", 26.032499999999999, 50.510599999999997, "Bahrain International Circuit", 1 },
                    { 2, "Jeddah", "Saudi Arabia", 21.631900000000002, 39.104399999999998, "Jeddah Corniche Circuit", 2 },
                    { 3, "Melbourne", "Australia", -37.849699999999999, 144.96799999999999, "Albert Park Circuit", 3 },
                    { 4, "Suzuka", "Japan", 34.8431, 136.54069999999999, "Suzuka International Racing Course", 4 },
                    { 5, "Shanghai", "China", 31.338899999999999, 121.2197, "Shanghai International Circuit", 5 },
                    { 6, "Miami", "United States", 25.958100000000002, -80.238900000000001, "Miami International Autodrome", 6 },
                    { 7, "Imola", "Italy", 44.343899999999998, 11.716699999999999, "Autodromo Enzo e Dino Ferrari", 7 },
                    { 8, "Monte Carlo", "Monaco", 43.734699999999997, 7.4206000000000003, "Circuit de Monaco", 8 },
                    { 9, "Barcelona", "Spain", 41.57, 2.2610999999999999, "Circuit de Barcelona-Catalunya", 9 },
                    { 10, "Montreal", "Canada", 45.5, -73.522800000000004, "Circuit Gilles Villeneuve", 10 },
                    { 11, "Spielberg", "Austria", 47.219700000000003, 14.764699999999999, "Red Bull Ring", 11 },
                    { 12, "Silverstone", "United Kingdom", 52.078600000000002, -1.0168999999999999, "Silverstone Circuit", 12 },
                    { 13, "Budapest", "Hungary", 47.578899999999997, 19.2486, "Hungaroring", 13 },
                    { 14, "Spa", "Belgium", 50.437199999999997, 5.9714, "Circuit de Spa-Francorchamps", 14 },
                    { 15, "Zandvoort", "Netherlands", 52.388800000000003, 4.5408999999999997, "Circuit Zandvoort", 15 },
                    { 16, "Monza", "Italy", 45.615600000000001, 9.2811000000000003, "Autodromo Nazionale Monza", 16 },
                    { 17, "Baku", "Azerbaijan", 40.372500000000002, 49.853299999999997, "Baku City Circuit", 17 },
                    { 18, "Singapore", "Singapore", 1.2914000000000001, 103.8639, "Marina Bay Street Circuit", 18 },
                    { 19, "Austin", "United States", 30.1328, -97.641099999999994, "Circuit of the Americas", 19 },
                    { 20, "Mexico City", "Mexico", 19.404199999999999, -99.090699999999998, "Autodromo Hermanos Rodriguez", 20 },
                    { 21, "São Paulo", "Brazil", -23.703600000000002, -46.6997, "Autodromo Jose Carlos Pace", 21 },
                    { 22, "Las Vegas", "United States", 36.114699999999999, -115.1728, "Las Vegas Street Circuit", 22 },
                    { 23, "Lusail", "Qatar", 25.489999999999998, 51.4542, "Losail International Circuit", 23 },
                    { 24, "Abu Dhabi", "UAE", 24.467199999999998, 54.603099999999998, "Yas Marina Circuit", 24 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "Country", "Name", "TeamPrincipal" },
                values: new object[,]
                {
                    { 1, "Milton Keynes", "Austria", "Red Bull Racing", "Christian Horner" },
                    { 2, "Maranello", "Italy", "Ferrari", "Frederic Vasseur" },
                    { 3, "Brackley", "Germany", "Mercedes", "Toto Wolff" },
                    { 4, "Woking", "United Kingdom", "McLaren", "Andrea Stella" },
                    { 5, "Silverstone", "United Kingdom", "Aston Martin", "Mike Krack" },
                    { 6, "Enstone", "France", "Alpine", "Oliver Oakes" },
                    { 7, "Grove", "United Kingdom", "Williams", "James Vowles" },
                    { 8, "Kannapolis", "United States", "Haas", "Ayao Komatsu" },
                    { 9, "Faenza", "Italy", "RB", "Laurent Mekies" },
                    { 10, "Hinwil", "Switzerland", "Kick Sauber", "Alessandro Alunni Bravi" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FullName", "IsActive", "Nationality", "Number", "Points", "TeamId" },
                values: new object[,]
                {
                    { 1, "Max Verstappen", true, "Dutch", 1, 437, 1 },
                    { 2, "Sergio Perez", true, "Mexican", 11, 152, 1 },
                    { 3, "Charles Leclerc", true, "Monegasque", 16, 356, 2 },
                    { 4, "Carlos Sainz", true, "Spanish", 55, 290, 2 },
                    { 5, "Lewis Hamilton", true, "British", 44, 234, 3 },
                    { 6, "George Russell", true, "British", 63, 217, 3 },
                    { 7, "Lando Norris", true, "British", 4, 374, 4 },
                    { 8, "Oscar Piastri", true, "Australian", 81, 292, 4 },
                    { 9, "Fernando Alonso", true, "Spanish", 14, 70, 5 },
                    { 10, "Lance Stroll", true, "Canadian", 18, 24, 5 },
                    { 11, "Pierre Gasly", true, "French", 10, 42, 6 },
                    { 12, "Esteban Ocon", true, "French", 31, 58, 6 },
                    { 13, "Alexander Albon", true, "Thai", 23, 12, 7 },
                    { 14, "Logan Sargeant", true, "American", 2, 6, 7 },
                    { 15, "Kevin Magnussen", true, "Danish", 20, 16, 8 },
                    { 16, "Nico Hulkenberg", true, "German", 27, 31, 8 },
                    { 17, "Daniel Ricciardo", true, "Australian", 3, 12, 9 },
                    { 18, "Yuki Tsunoda", true, "Japanese", 22, 22, 9 },
                    { 19, "Valtteri Bottas", true, "Finnish", 77, 0, 10 },
                    { 20, "Zhou Guanyu", true, "Chinese", 24, 6, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Circuits",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
