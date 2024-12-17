using MicroservicioProductos.Application.Services.Interfaces;
using MicroservicioProductos.Infrastructure.Models;
using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioProductos.API.enpoints;

public static class MovimientoEndpoints
{
    public static void MapMovimientoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/movimientos", async (HttpContext context) =>
            {
                var serviceProvider = context.RequestServices;
                var movimientoService = serviceProvider.GetRequiredService<IMovimientoService>();
                var movimiento = await context.Request.ReadFromJsonAsync<Movimiento>();

                if (movimiento == null)
                {
                    return Results.BadRequest("Invalid movimiento data");
                }

                var result = await movimientoService.RegistrarMovimientoAsync(movimiento);
                return Results.Created($"/movimientos/{result.Id}", result);
            })
            .WithName("CreateMovimiento")
            .WithOpenApi();

        endpoints.MapGet("/movimientos", async (ApplicationDbContext context) =>
            {
                return Results.Ok(await context.Movimientos.ToListAsync());
            })
            .WithName("GetMovimientos")
            .WithOpenApi();

        endpoints.MapGet("/movimientos/{id}", async (ApplicationDbContext context, int id) =>
            {
                var movimiento = await context.Movimientos.FindAsync(id);
                return movimiento is not null ? Results.Ok(movimiento) : Results.NotFound();
            })
            .WithName("GetMovimientoById")
            .WithOpenApi();

        endpoints.MapPut("/movimientos/{id}", async (ApplicationDbContext context, int id, Movimiento updatedMovimiento) =>
            {
                var movimiento = await context.Movimientos.FindAsync(id);
                if (movimiento is null) return Results.NotFound();

                movimiento.Id = updatedMovimiento.Id;
                movimiento.Monto = updatedMovimiento.Monto;
                movimiento.Fecha = updatedMovimiento.Fecha;

                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("UpdateMovimiento")
            .WithOpenApi();

        endpoints.MapDelete("/movimientos/{id}", async (ApplicationDbContext context, int id) =>
            {
                var movimiento = await context.Movimientos.FindAsync(id);
                if (movimiento is null) return Results.NotFound();

                context.Movimientos.Remove(movimiento);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteMovimiento")
            .WithOpenApi();
    }
}