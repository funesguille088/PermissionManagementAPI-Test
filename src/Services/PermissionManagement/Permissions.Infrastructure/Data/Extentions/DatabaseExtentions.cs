using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Permissions.Infrastructure.Data.Extentions
{
    public static class DatabaseExtentions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedEmployeeAsync(context);
            await SeedPermissionsAsync(context);
        }

        private static async Task SeedEmployeeAsync(ApplicationDbContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                await context.Employees.AddRangeAsync(InitialData.Employees);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedPermissionsAsync(ApplicationDbContext context)
        {
            if (!await context.Permissions.AnyAsync())
            {
                await context.Permissions.AddRangeAsync(InitialData.Permissions);
                await context.SaveChangesAsync();
            }
        }

    }
}
