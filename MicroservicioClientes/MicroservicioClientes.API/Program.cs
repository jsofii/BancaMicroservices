using MicroservicioClientes.Application.Services;
using MicroservicioClientes.Domain;
using MicroservicioClientes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Enable HTTPS redirection (optional, remove if not required in Docker)
app.UseHttpsRedirection();

// Force the app to bind to port 80 (necessary for Docker)
app.Urls.Add("http://*:80");

app.MapMovimientosEndpoints();

// Run the application
app.Run();

