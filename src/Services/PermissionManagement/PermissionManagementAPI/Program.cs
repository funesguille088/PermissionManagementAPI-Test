using BuildingBlocks.Behaviors;
using Nest;
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
LoggingConfiguration logging = new LoggingConfiguration();
logging.configureLogging();
builder.Host.UseSerilog();
builder.Services.AddSingleton<IElasticClient>(logging.ConfigureElasticClient());


builder.Services.AddScoped<PermissionSyncService>();


builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


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
