/*
 * This class provides extension methods to initialize the database with seed data.
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Permissions.Infrastructure.Data.Extentions
{
    public static class DatabaseExtentions
    {
        // Extension method to initialize the database asynchronously
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Apply any pending migrations to the database
            context.Database.MigrateAsync().GetAwaiter().GetResult();

            // Seed the database with initial data
            await SeedAsync(context);
        }

        // Method to seed the database with initial data
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            // Seed employees if the table is empty
            await SeedEmployeeAsync(context);

            // Seed permissions if the table is empty
            await SeedPermissionsAsync(context);
        }

        // Method to seed employees
        private static async Task SeedEmployeeAsync(ApplicationDbContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                await context.Employees.AddRangeAsync(InitialData.Employees);
                await context.SaveChangesAsync();
            }
        }
        
        // Method to seed permissions
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
