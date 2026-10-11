using E_Commerce.Domain.Contracts;

namespace GymManagementApp.API.Extentions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedMigrationDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var identitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await dataSeeder.SeedDataAsync();
            await identitySeeder.SeedDataAsync();
            return app;
        }
    }
}
