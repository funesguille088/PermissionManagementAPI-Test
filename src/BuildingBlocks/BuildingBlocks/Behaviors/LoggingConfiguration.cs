using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using Nest;
using System;

namespace BuildingBlocks.Behaviors
{
    public class LoggingConfiguration
    {
        private const string IndexName = "permissionregistry";
        public void configureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration,environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string? environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".","-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2, 
                NumberOfReplicas = 2
            }; 
        }

        public ElasticClient ConfigureElasticClient()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

            var elasticsearchUri = new Uri(configuration["ElasticConfiguration:Uri"]);
            var settings = new ConnectionSettings(elasticsearchUri)
                .DefaultIndex(IndexName)
                //.DefaultMappingFor(m => m.IndexName(IndexName))
                .EnableHttpPipelining()
                .DisableDirectStreaming()
                .EnableDebugMode()
                .EnableApiVersioningHeader();
            var elsClient = new ElasticClient(settings);
            
            return elsClient;
        }
    }
}
