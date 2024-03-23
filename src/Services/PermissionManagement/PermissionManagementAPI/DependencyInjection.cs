/*
   The DependencyInjection class provides methods for configuring and using API services.

   Usage:
   - AddApiServices: Adds Carter as a service.
   - UseApiServices: Configures Carter for the WebApplication.
*/

namespace PermissionManagementAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}
