using FormulaOne.Api.Contexts;
using FormulaOne.Api.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CircuitsController : ControllerBase {
    private readonly AppDbContext _context;

    public CircuitsController(AppDbContext context) {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CircuitResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CircuitResponseDto>>> GetCircuits() {
        var circuits = await _context.Circuits
            .OrderBy(c => c.RoundNumber)
            .Select(c => new CircuitResponseDto {
                Id = c.Id,
                Name = c.Name,
                Country = c.Country,
                City = c.City,
                Lat = c.Lat,
                Lng = c.Lng,
                RoundNumber = c.RoundNumber
            })
            .ToListAsync();

        return Ok(circuits);
    }
}
