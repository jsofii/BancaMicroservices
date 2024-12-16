using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Application.Exceptions;
using MicroservicioProductos.Application.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MicroservicioProductos.Infrastructure.Persistence;
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

                try
                {
                    var result = await movimientoService.RegistrarMovimientoAsync(movimiento);
                    return Results.Created($"/movimientos/{result.MovimientoId}", result);
                }
                catch (InsufficientBalanceException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            })
            .WithName("CreateMovimiento")
            .WithOpenApi();    
        
        
        endpoints.MapPost("/movimientos", async (ApplicationDbContext context, Movimiento movimiento) =>
        {
            context.Movimientos.Add(movimiento);
            await context.SaveChangesAsync();
            return Results.Created($"/movimientos/{movimiento.MovimientoId}", movimiento);
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

            movimiento.CuentaId = updatedMovimiento.CuentaId;
            movimiento.Valor = updatedMovimiento.Valor;
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