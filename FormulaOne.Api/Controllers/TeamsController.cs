using FormulaOne.Api.Contexts;
using FormulaOne.Api.Entities.Database;
using FormulaOne.Api.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamResponseDto>>> GetTeams([FromQuery] string? search) {
            var query = _context.Teams.Include(t => t.Drivers).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(t =>
                    t.Name.Contains(search) ||
                    t.TeamPrincipal.Contains(search) ||
                    t.Country.Contains(search) ||
                    t.City.Contains(search));
            }

            var teams = await query.Select(t => new TeamResponseDto {
                Id = t.Id,
                Name = t.Name,
                TeamPrincipal = t.TeamPrincipal,
                Country = t.Country,
                City = t.City,
                DriverCount = t.Drivers.Count
            }).ToListAsync();

            return Ok(teams);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeamResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamResponseDto>> GetTeam(int id) {
            var team = await _context.Teams.Include(t => t.Drivers).FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();

            var teamDto = new TeamResponseDto{
                    Id = team.Id,
                    Name = team.Name,
                    TeamPrincipal = team.TeamPrincipal,
                    Country = team.Country,
                    City = team.City,
                    DriverCount = team.Drivers.Count
            };

            return Ok(teamDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TeamResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamResponseDto>> CreateTeam(TeamRequestDto dto) {
            var team = new Team {
                Name = dto.Name,
                TeamPrincipal = dto.TeamPrincipal,
                Country = dto.Country,
                City = dto.City
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            var teamDto = new TeamResponseDto {
                Id = team.Id,
                Name = team.Name,
                TeamPrincipal = team.TeamPrincipal,
                Country = team.Country,
                City = team.City,
                DriverCount = 0
            };

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, teamDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeam(int id, TeamRequestDto dto) {
            var team = await _context.Teams.FindAsync(id);
            if(team == null) { return NotFound(); }

            team.Name = dto.Name;
            team.TeamPrincipal = dto.TeamPrincipal;
            team.Country = dto.Country;
            team.City = dto.City;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteTeam(int id) {
            var team = await _context.Teams
                .Include(t => t.Drivers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) { return NotFound(); }

            if (team.Drivers.Any()) { return Conflict( new {
                message = "Cannot delete a team that still has drivers assigned to it.",
                drivers = team.Drivers.Select(d => new { d.Id, d.FullName, d.Number })
            }); 
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
