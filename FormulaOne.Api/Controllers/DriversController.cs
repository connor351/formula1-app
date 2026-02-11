using FormulaOne.Api.Contexts;
using FormulaOne.Api.Entities.Database;
using FormulaOne.Api.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase {

        private readonly AppDbContext _context;

        public DriversController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DriverResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetDrivers( [FromQuery] string? search, [FromQuery] string? sortBy) {
            var query = _context.Drivers.Include(d => d.Team).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search)) { 
                query = query.Where(d => 
                d.FullName.Contains(search) || 
                d.Nationality.Contains(search) || 
                d.Team.Name.Contains(search));
            }

            query = sortBy switch {
                "name" => query.OrderBy(d => d.FullName),
                "number" => query.OrderBy(d => d.Number),
                "points" => query.OrderByDescending(d => d.Points),
                "team" => query.OrderBy(d => d.Team.Name),
                _ => query
            };

            var drivers = await query.Select(d => new DriverResponseDto {
                Id = d.Id,
                FullName = d.FullName,
                Number = d.Number,
                Nationality = d.Nationality,
                IsActive = d.IsActive,
                Points = d.Points,
                TeamId = d.TeamId,
                TeamName = d.Team.Name
            }).ToListAsync();

            return Ok(drivers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DriverResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverResponseDto>> GetDriver(int id) {
            var driver = await _context.Drivers.Include(d => d.Team).FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null) { return NotFound(); }

            var driverDto = new DriverResponseDto {
                Id = driver.Id,
                FullName = driver.FullName,
                Number = driver.Number,
                Nationality = driver.Nationality,
                IsActive = driver.IsActive,
                Points = driver.Points,
                TeamId = driver.TeamId,
                TeamName = driver.Team.Name
            };

            return Ok(driverDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DriverResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<DriverResponseDto>> CreateDriver(DriverRequestDto dto) {

            // Already enforced in DTO using DataAnnotations, but was causing unit test to fail since it calls method directly and bypasses DataAnnotations.
            if (dto.Points < 0) { return BadRequest("Points cannot be negative."); }

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == dto.TeamId);
            if (!teamExists) { return BadRequest("The specified team does not exist."); }

            var numberTaken = await _context.Drivers.AnyAsync(d => d.Number == dto.Number);
            if (numberTaken) { return Conflict($"Driver number {dto.Number} is already taken."); }

            var driver = new Driver {
                FullName = dto.FullName,
                Number = dto.Number,
                Nationality = dto.Nationality,
                IsActive = dto.IsActive,
                Points = dto.Points,
                TeamId = dto.TeamId
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            var team = await _context.Teams.FindAsync(driver.TeamId);

            var driverDto = new DriverResponseDto {
                Id = driver.Id,
                FullName = driver.FullName,
                Number = driver.Number,
                Nationality = driver.Nationality,
                IsActive = driver.IsActive,
                Points = driver.Points,
                TeamId = driver.TeamId,
                TeamName = team!.Name
            };

            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driverDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateDriver(int id, DriverRequestDto dto) {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) { return NotFound(); }

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == dto.TeamId);
            if (!teamExists) { return BadRequest("The specified team does not exist."); }

            var numberTaken = await _context.Drivers.AnyAsync(d => d.Number == dto.Number && d.Id != id);
            if (numberTaken) { return Conflict($"Driver number {dto.Number} is already taken."); }

            driver.FullName = dto.FullName;
            driver.Number = dto.Number;
            driver.Nationality = dto.Nationality;
            driver.IsActive = dto.IsActive;
            driver.Points = dto.Points;
            driver.TeamId = dto.TeamId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDriver(int id) {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
