
using BuildingBlocks.Behaviors;
using Elasticsearch.Net;
/*
   The DependencyInjection class provides extension methods to configure application services.

   Usage:
   1. Call the AddApplicationServices method on IServiceCollection to configure application services.
   2. Optionally, configure the ElasticSearch client by calling the ConfigureElasticClient method.
*/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Permissions.Domain.Models;
using System.Reflection;

namespace Permissions.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR with behaviors
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });


            // Register ElasticLowLevelClient as a transient service
            services.AddTransient<IElasticLowLevelClient>(e => ConfigureElasticClient());

            return services;
        }

        private static ElasticLowLevelClient ConfigureElasticClient()
        {
            const string IndexName = "permissionregistry";
            
            // Retrieve environment variables
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build configuration from appsettings.json and environment-specific JSON file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

            // Get Elasticsearch URI from configuration
            var elasticsearchUri = new Uri(configuration["ElasticConfiguration:Uri"]);

            // Configure Elasticsearch settings
            var settings = new ConnectionSettings(elasticsearchUri)
                .DefaultMappingFor<ELSPermissionDocument>(i => i
                .IndexName(IndexName)
                .IdProperty(p => p.permissionid))
                .EnableDebugMode()
                .EnableHttpPipelining()
                .DisableAutomaticProxyDetection()
                .EnableHttpCompression()
                .DisableDirectStreaming()
                .DisableDirectStreaming()
                .EnableDebugMode()
                .EnableApiVersioningHeader()
                .PrettyJson();

            // Create and return ElasticLowLevelClient
            var elsClient = new ElasticLowLevelClient(settings);

            return elsClient;
        }
    }
}
