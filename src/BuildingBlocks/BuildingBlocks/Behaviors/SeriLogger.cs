/*
 * SeriLogger: A static class for configuring Serilog logging in .NET applications.
 * 
 * This class provides a static method for configuring Serilog logger using Serilog's fluent API.
 * It configures enrichers, sinks, and other settings for logging to different targets like Debug, Console, and Elasticsearch.
 * 
 * Usage:
 * Call the Configure method with HostBuilderContext and LoggerConfiguration parameters to configure Serilog.
 */

using Microsoft.Extensions.Configuration; // Importing Microsoft.Extensions.Configuration for accessing configuration settings
using Serilog; // Importing Serilog library for logging capabilities
using Serilog.Exceptions; // Importing Serilog.Exceptions for enriching logs with exception details
using Serilog.Sinks.Elasticsearch; // Importing Serilog.Sinks.Elasticsearch for logging to Elasticsearch
using Nest; // Importing Nest for Elasticsearch client
using Microsoft.Extensions.Hosting; // Importing Microsoft.Extensions.Hosting for accessing hosting environment information
using System; // Importing System for DateTime

namespace BuildingBlocks.Behaviors
{
    public static class SeriLogger
    {
        // Configure method to set up Serilog logger
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               // Retrieve Elasticsearch URI from configuration
               var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

               // Configure Serilog logger with enrichers, sinks, and other settings
               configuration
                    .Enrich.FromLogContext() // Enrich log events with contextual information
                    .Enrich.WithMachineName() // Enrich log events with machine name
                    .Enrich.WithExceptionDetails() // Enrich log events with exception details
                    .WriteTo.Debug() // Write log events to debug output
                    .WriteTo.Console() // Write log events to console
                    .WriteTo.Elasticsearch( // Write log events to Elasticsearch
                        new ElasticsearchSinkOptions(new Uri(elasticUri))
                        {
                            IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        })
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName) // Enrich log events with environment name
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName) // Enrich log events with application name
                    .ReadFrom.Configuration(context.Configuration); // Read additional configuration from appsettings.json
           };
    }
}
