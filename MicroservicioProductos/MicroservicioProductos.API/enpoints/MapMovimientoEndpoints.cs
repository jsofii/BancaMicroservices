// MovimientoEndpoints.cs

using FluentValidation;
using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

// MovimientoEndpoints.cs
// MovimientoEndpoints.cs

using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class MovimientoEndpoints
{
public static void MapMovimientoEndpoints(this IEndpointRouteBuilder endpoints)
{
    endpoints.MapPost("/movimientos", async (ApplicationDbContext context, 
            MovimientoDto movimientoDto, IValidator<MovimientoDto> validator) =>
    {
        var validationResult = await validator.ValidateAsync(movimientoDto);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        if (!context.Cuentas.Any(c => c.Id == movimientoDto.CuentaId))
        {
            return Results.BadRequest("Invalid CuentaId.");
        }

        var movimiento = movimientoDto.ToEntity();
        context.Movimientos.Add(movimiento);
        await context.SaveChangesAsync();
        return Results.Created($"/movimientos/{movimiento.Id}", movimientoDto);
    })
    .WithName("CreateMovimiento")
    .WithOpenApi();

    endpoints.MapGet("/movimientos/{id}", async (ApplicationDbContext context, int id) =>
    {
        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null) return Results.NotFound("Movimiento not found.");
        var movimientoDto = MovimientoDto.ToDto(movimiento);
        return Results.Ok(movimientoDto);
    })
    .WithName("GetMovimientoById")
    .WithOpenApi();

    endpoints.MapPut("/movimientos/{id}", async (ApplicationDbContext context, int id,
            MovimientoDto updatedMovimientoDto, IValidator<MovimientoDto> validator) =>
    {
        var validationResult = await validator.ValidateAsync(updatedMovimientoDto);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null) return Results.NotFound("Movimiento not found.");
        movimiento.Fecha = updatedMovimientoDto.Fecha;
        movimiento.Monto = updatedMovimientoDto.Monto;
        await context.SaveChangesAsync();
        return Results.NoContent();
    })
    .WithName("UpdateMovimiento")
    .WithOpenApi();

    endpoints.MapGet("/movimientos", async (ApplicationDbContext context) =>
    {
        var movimientos = await context.Movimientos
            .Select(m => MovimientoDto.ToDto(m))
            .ToListAsync();
        return Results.Ok(movimientos);
    })
    .WithName("GetAllMovimientos")
    .WithOpenApi();
}}