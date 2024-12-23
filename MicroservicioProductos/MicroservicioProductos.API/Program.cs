using FluentValidation;
using MicroservicioProductos.Application.Validators;
using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<MovimientoDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Enable HTTPS redirection (optional, remove if not required in Docker)
app.UseHttpsRedirection();

// Force the app to bind to port 80 (necessary for Docker)
app.Urls.Add("http://*:80");

app.MapMovimientoEndpoints();
app.MapCuentaEndpoints();
app.MapReportEndpoints();



// Run the application
app.Run();

// Define the WeatherForecast record
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}