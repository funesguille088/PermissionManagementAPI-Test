/*
   The startup code configures and runs the Permission Management API.

   Usage:
   - AddControllers: Adds controllers to the service container.
   - AddEndpointsApiExplorer: Adds API explorer services.
   - AddSwaggerGen: Adds Swagger services.
   - UseSerilog: Configures Serilog for logging.
   - AddApplicationServices: Configures application services.
   - AddInfrastructureServices: Configures infrastructure services.
   - AddApiServices: Configures API services.
   - UseApiServices: Uses configured API services.
   - InitializeDatabaseAsync: Initializes the database asynchronously.
   - UseSwagger: Configures Swagger middleware.
   - UseSwaggerUI: Configures Swagger UI middleware.
   - UseHttpsRedirection: Enables HTTPS redirection.
   - UseAuthorization: Adds authorization middleware.
   - MapControllers: Maps controller routes.
   - Run: Runs the application.
*/

using BuildingBlocks.Behaviors;
using PermissionManagementAPI;
using Permissions.Application;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;
using Permissions.Infrastructure;
using Permissions.Infrastructure.Data.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// configure serilog

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

builder.Services.AddTransient<PermissionSyncService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseApiServices();


if (app.Environment.IsDevelopment())
{
    //Calling migration method to create entities into SQL server database tables
    await app.InitializeDatabaseAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();