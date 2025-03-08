using Inventory.Management.System.API.SystemConfiguration;
using Inventory.Management.System.Logic.Extensions.DependencyConfiguration;
using Inventory.Management.System.Logic.Settings;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Add system services
//configure db
var dbConnections = new SQLServerSettings();
builder.Configuration.GetSection(nameof(SQLServerSettings)).Bind(dbConnections);
builder.Services.ConfigureDatabaseSQLServer(builder.Environment, dbConnections.ConnectionString);
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog for logging
// Ensure log directory exists
var logPath = @"C:\CraftSilicon\inventoryManagementSystem\logs";
Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console() // Always log to console
    .WriteTo.File(Path.Combine(logPath, "app.log"), rollingInterval: RollingInterval.Day) // Log to file in production
    .CreateLogger();


// Add Serilog to the DI container
builder.Host.UseSerilog(); // Ensure this is before `builder.Build()
//configure system services
builder.Services.ConfigureSystemServices(builder.Environment, builder.Configuration, dbConnections.ConnectionString);

var app = builder.Build();

app.UseSerilogRequestLogging(); // Log HTTP requests

// Enable Swagger in all environments (including production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

