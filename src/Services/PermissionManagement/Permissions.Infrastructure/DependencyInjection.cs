/*
 * This class contains an extension method to add infrastructure-related services to the IServiceCollection.
 * It configures and registers services such as DbContext and interceptors.
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Permissions.Infrastructure.Data;
using Permissions.Infrastructure.Data.Interceptors;
using Permissions.Application.Data;

namespace Permissions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        // Get the connection string from configuration
        var connectionString = configuration.GetConnectionString("Database");

        // Add interceptors for saving changes to the container
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Configure the ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            // Add the registered interceptors to DbContext options
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            // Use SQL Server as the database provider with the provided connection string
            options.UseSqlServer(connectionString);
        });

        // Register the ApplicationDbContext with IApplicationDbContext
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        return services;
    }

}
