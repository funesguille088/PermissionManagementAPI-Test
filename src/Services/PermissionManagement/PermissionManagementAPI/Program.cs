using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Hosting;
using Nest;
using PermissionManagementAPI;
using Permissions.Application;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;
using Permissions.Infrastructure;
using Permissions.Infrastructure.Data.Extentions;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

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