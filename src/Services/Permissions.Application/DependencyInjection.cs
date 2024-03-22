
using BuildingBlocks.Behaviors;
using Elasticsearch.Net;
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
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            //services.AddSingleton<IElasticLowLevelClient>(ConfigureElasticClient());

            services.AddTransient<IElasticLowLevelClient>(e => ConfigureElasticClient());

            return services;
        }

        private static ElasticLowLevelClient ConfigureElasticClient()
        {
            const string IndexName = "permissionregistry";

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

            var elasticsearchUri = new Uri(configuration["ElasticConfiguration:Uri"]);
            
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
            
            var elsClient = new ElasticLowLevelClient(settings);

            return elsClient;
        }
    }
}
