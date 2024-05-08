using EVerywhere.ChargerPoint.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EVerywhere.ChargerPoint.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseChargerPointDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initializer.InitialiseAsync();

    }
}

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
    IChargerPointDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.MigrateDatabase();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}