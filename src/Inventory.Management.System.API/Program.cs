using Inventory.Management.System.Logic.Extensions.DependencyConfiguration;
using Inventory.Management.System.Logic.Settings;

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

var app = builder.Build();

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

