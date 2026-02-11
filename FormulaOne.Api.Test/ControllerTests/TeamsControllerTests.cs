using FormulaOne.Api.Contexts;
using FormulaOne.Api.Controllers;
using FormulaOne.Api.Entities.Database;
using FormulaOne.Api.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TeamsControllerTests {
    private AppDbContext GetInMemoryContext() {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetTeams_NoSearch_ReturnsAllTeams() {
        var context = GetInMemoryContext();
        context.Teams.AddRange(
            new Team { Id = 1, Name = "Red Bull Racing", TeamPrincipal = "Christian Horner", Country = "Austria", City = "Milton Keynes" },
            new Team { Id = 2, Name = "Ferrari", TeamPrincipal = "Frederic Vasseur", Country = "Italy", City = "Maranello" }
        );
        await context.SaveChangesAsync();

        var controller = new TeamsController(context);
        var result = await controller.GetTeams(null);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var teams = Assert.IsAssignableFrom<IEnumerable<TeamResponseDto>>(ok.Value);
        Assert.Equal(2, teams.Count());
    }

    [Fact]
    public async Task GetTeams_SearchByName_ReturnsMatchingTeams() {
        var context = GetInMemoryContext();
        context.Teams.AddRange(
            new Team { Id = 1, Name = "Red Bull Racing", TeamPrincipal = "Christian Horner", Country = "Austria", City = "Milton Keynes" },
            new Team { Id = 2, Name = "Ferrari", TeamPrincipal = "Frederic Vasseur", Country = "Italy", City = "Maranello" }
        );
        await context.SaveChangesAsync();

        var controller = new TeamsController(context);
        var result = await controller.GetTeams("Ferrari");

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var teams = Assert.IsAssignableFrom<IEnumerable<TeamResponseDto>>(ok.Value);
        Assert.Single(teams);
        Assert.Equal("Ferrari", teams.First().Name);
    }

    [Fact]
    public async Task GetTeam_ValidId_ReturnsTeamWithDriverCount() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull Racing", TeamPrincipal = "Christian Horner", Country = "Austria", City = "Milton Keynes" });
        context.Drivers.AddRange(
            new Driver { Id = 1, FullName = "Max Verstappen", Number = 1, Nationality = "Dutch", TeamId = 1 },
            new Driver { Id = 2, FullName = "Sergio Perez", Number = 11, Nationality = "Mexican", TeamId = 1 }
        );
        await context.SaveChangesAsync();

        var controller = new TeamsController(context);
        var result = await controller.GetTeam(1);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var team = Assert.IsType<TeamResponseDto>(ok.Value);
        Assert.Equal(2, team.DriverCount);
    }

    [Fact]
    public async Task GetTeam_InvalidId_ReturnsNotFound() {
        var context = GetInMemoryContext();
        var controller = new TeamsController(context);

        var result = await controller.GetTeam(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateTeam_ValidDto_ReturnsCreatedWithTeam() {
        var context = GetInMemoryContext();
        var controller = new TeamsController(context);
        var dto = new TeamRequestDto {
            Name = "McLaren",
            TeamPrincipal = "Andrea Stella",
            Country = "United Kingdom",
            City = "Woking"
        };

        var result = await controller.CreateTeam(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var team = Assert.IsType<TeamResponseDto>(created.Value);
        Assert.Equal("McLaren", team.Name);
        Assert.Equal(0, team.DriverCount);
    }

    [Fact]
    public async Task UpdateTeam_InvalidId_ReturnsNotFound() {
        var context = GetInMemoryContext();
        var controller = new TeamsController(context);
        var dto = new TeamRequestDto {
            Name = "McLaren",
            TeamPrincipal = "Andrea Stella",
            Country = "United Kingdom",
            City = "Woking"
        };

        var result = await controller.UpdateTeam(999, dto);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}