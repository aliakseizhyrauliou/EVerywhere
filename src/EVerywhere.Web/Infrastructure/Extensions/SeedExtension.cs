using EVerywhere.Balance.Infrastructure.Data;
using EVerywhere.ChargerPoint.Infrastructure.Data;

namespace EVerywhere.Web.Infrastructure.Extensions;

public static class SeedExtension
{
    public static async Task SeedAllDatabases(this WebApplication app)
    {
        await app.InitialiseBalanceDatabaseAsync();
        await app.InitialiseChargerPointDatabaseAsync();
    }
}