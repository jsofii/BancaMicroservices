// MovimientoEndpoints.cs

using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class MovimientoEndpoints
{
public static void MapMovimientoEndpoints(this IEndpointRouteBuilder endpoints)
{
    endpoints.MapPost("/movimientos", async (ApplicationDbContext context, MovimientoDto movimientoDto) =>
    {
        var movimiento = movimientoDto.ToEntity();
        context.Movimientos.Add(movimiento);
        await context.SaveChangesAsync();
        return Results.Created($"/movimientos/{movimiento.Id}", movimientoDto);
    })
    .WithName("CreateMovimiento")
    .WithOpenApi();

    endpoints.MapGet("/movimientos", async (ApplicationDbContext context) =>
    {
        var movimientos = await context.Movimientos.ToListAsync();
        var movimientoDtos = movimientos.Select(MovimientoDto.ToDto).ToList();
        return Results.Ok(movimientoDtos);
    })
    .WithName("GetMovimientos")
    .WithOpenApi();

    endpoints.MapGet("/movimientos/{id}", async (ApplicationDbContext context, int id) =>
    {
        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null) return Results.NotFound();
        var movimientoDto = MovimientoDto.ToDto(movimiento);
        return Results.Ok(movimientoDto);
    })
    .WithName("GetMovimientoById")
    .WithOpenApi();

    endpoints.MapPut("/movimientos/{id}", async (ApplicationDbContext context, int id, MovimientoDto updatedMovimientoDto) =>
    {
        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null) return Results.NotFound();
        movimiento.NumeroCuenta = updatedMovimientoDto.NumeroCuenta;
        movimiento.Fecha = updatedMovimientoDto.Fecha;
        movimiento.Monto = updatedMovimientoDto.Monto;
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
}}