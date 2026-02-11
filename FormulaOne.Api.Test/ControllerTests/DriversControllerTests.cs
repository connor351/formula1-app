// DriversControllerTests.cs

using Microsoft.EntityFrameworkCore;
using System;
using FormulaOne.Api.Controllers;
using FormulaOne.Api.Contexts;
using FormulaOne.Api.Entities.Database;
using FormulaOne.Api.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

public class DriversControllerTests {
    private AppDbContext GetInMemoryContext() {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateDriver_DuplicateNumber_ReturnsConflict() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull", TeamPrincipal = "Horner", Country = "Austria", City = "Milton Keynes" });
        context.Drivers.Add(new Driver { Id = 1, FullName = "Max Verstappen", Number = 1, Nationality = "Dutch", TeamId = 1 });
        await context.SaveChangesAsync();

        var controller = new DriversController(context);
        var dto = new DriverRequestDto { FullName = "Test Driver", Number = 1, Nationality = "British", TeamId = 1 };

        var result = await controller.CreateDriver(dto);

        Assert.IsType<ConflictObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDriver_NegativePoints_ReturnsBadRequest() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull", TeamPrincipal = "Horner", Country = "Austria", City = "Milton Keynes" });
        await context.SaveChangesAsync();

        var controller = new DriversController(context);
        var dto = new DriverRequestDto { FullName = "Test Driver", Number = 99, Nationality = "British", Points = -10, TeamId = 1 };

        var result = await controller.CreateDriver(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDriver_InvalidTeam_ReturnsBadRequest() {
        var context = GetInMemoryContext();
        var controller = new DriversController(context);
        var dto = new DriverRequestDto { FullName = "Test Driver", Number = 99, Nationality = "British", TeamId = 999 };

        var result = await controller.CreateDriver(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteTeam_WithDrivers_ReturnsConflict() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull", TeamPrincipal = "Horner", Country = "Austria", City = "Milton Keynes" });
        context.Drivers.Add(new Driver { Id = 1, FullName = "Max Verstappen", Number = 1, Nationality = "Dutch", TeamId = 1 });
        await context.SaveChangesAsync();

        var controller = new TeamsController(context);

        var result = await controller.DeleteTeam(1);

        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task DeleteTeam_WithNoDrivers_ReturnsNoContent() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull", TeamPrincipal = "Horner", Country = "Austria", City = "Milton Keynes" });
        await context.SaveChangesAsync();

        var controller = new TeamsController(context);

        var result = await controller.DeleteTeam(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetDrivers_SearchByName_ReturnsMatchingDrivers() {
        var context = GetInMemoryContext();
        context.Teams.Add(new Team { Id = 1, Name = "Red Bull", TeamPrincipal = "Horner", Country = "Austria", City = "Milton Keynes" });
        context.Drivers.AddRange(
            new Driver { Id = 1, FullName = "Max Verstappen", Number = 1, Nationality = "Dutch", TeamId = 1 },
            new Driver { Id = 2, FullName = "Lewis Hamilton", Number = 44, Nationality = "British", TeamId = 1 }
        );
        await context.SaveChangesAsync();

        var controller = new DriversController(context);

        var result = await controller.GetDrivers(search: "Hamilton", sortBy: null);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var drivers = Assert.IsAssignableFrom<IEnumerable<DriverResponseDto>>(ok.Value);
        Assert.Single(drivers);
        Assert.Equal("Lewis Hamilton", drivers.First().FullName);
    }
}