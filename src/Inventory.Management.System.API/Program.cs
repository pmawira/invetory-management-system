using Inventory.Management.System.API.Helpers;
using Inventory.Management.System.API.SystemConfiguration;
using Inventory.Management.System.Logic.Extensions.DependencyConfiguration;
using Inventory.Management.System.Logic.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthService>();

// Add services to the container.
builder.Services.AddControllers();
//Add system services
//configure db
var dbConnections = new SQLServerSettings();
builder.Configuration.GetSection(nameof(SQLServerSettings)).Bind(dbConnections);
builder.Services.ConfigureDatabaseSQLServer(builder.Environment, dbConnections.ConnectionString);
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory Management System API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below. Example: 'Bearer eyJhbGci...'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

