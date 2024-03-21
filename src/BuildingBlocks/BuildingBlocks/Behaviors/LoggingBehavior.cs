using MediatR;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using Serilog.Core;
using Microsoft.Extensions.Configuration;
using Serilog.Exceptions;
using System.Reflection;

namespace BuildingBlocks.Behaviors;

//public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : notnull, IRequest<TResponse>
//        where TResponse : notnull
//{
//    private readonly ILogger _logger;

//    public LoggingBehavior(ILogger logger)
//    {
//        _logger = logger;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var log = _logger.ForContext<LoggingBehavior<TRequest, TResponse>>();
//        log.Information("[START] Handle request = {Request} - Response = {Response} - RequestData = {RequestData}",
//            typeof(TRequest).Name, typeof(TResponse).Name, request);

//        var timer = new Stopwatch();
//        timer.Start();

//        var response = await next();

//        timer.Stop();
//        var timeTaken = timer.Elapsed;
//        if (timeTaken.Seconds > 3)
//        {
//            log.Warning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
//                typeof(TRequest).Name, timeTaken.Seconds);
//        }

//        log.Information("[END] Handled {Request} with {Response}",
//                typeof(TRequest).Name, typeof(TResponse).Name);

//        return response;
//    }
//}



public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
{
    private Serilog.ILogger _logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

        var log = Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                    .Enrich.WithProperty("Environment", environment)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

        log.Information("[START] Handle request = {Request} - Response = {Response} - RequestData = {RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);
        logger.LogInformation("[START] Handle request = {Request} - Response = {Response} - RequestData = {RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);
            log.Warning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);
        log.Information("[END] Handled {Request} with {Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);
        
        return response;

    }

    ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string? environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        {
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 2
        };
    }
}

