// CuentaEndpoints.cs

using MicroservicioClientes.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MicroservicioProductos.Infrastructure.Persistence;

public static class CuentaEndpoints
{
    public static void MapCuentaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/cuentas", async (ApplicationDbContext context, Cuenta cuenta) =>
        {
            context.Cuentas.Add(cuenta);
            await context.SaveChangesAsync();
            return Results.Created($"/cuentas/{cuenta.CuentaId}", cuenta);
        })
        .WithName("CreateCuenta")
        .WithOpenApi();

        endpoints.MapGet("/cuentas", async (ApplicationDbContext context) =>
        {
            return Results.Ok(await context.Cuentas.ToListAsync());
        })
        .WithName("GetCuentas")
        .WithOpenApi();

        endpoints.MapGet("/cuentas/{id}", async (ApplicationDbContext context, int id) =>
        {
            var cuenta = await context.Cuentas.FindAsync(id);
            return cuenta is not null ? Results.Ok(cuenta) : Results.NotFound();
        })
        .WithName("GetCuentaById")
        .WithOpenApi();

        endpoints.MapPut("/cuentas/{id}", async (ApplicationDbContext context, int id, Cuenta updatedCuenta) =>
        {
            var cuenta = await context.Cuentas.FindAsync(id);
            if (cuenta is null) return Results.NotFound();

            cuenta.NumeroDeCuenta = updatedCuenta.NumeroDeCuenta;
            cuenta.Estado = updatedCuenta.Estado;

            await context.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("UpdateCuenta")
        .WithOpenApi();

        endpoints.MapDelete("/cuentas/{id}", async (ApplicationDbContext context, int id) =>
        {
            var cuenta = await context.Cuentas.FindAsync(id);
            if (cuenta is null) return Results.NotFound();

            context.Cuentas.Remove(cuenta);
            await context.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("DeleteCuenta")
        .WithOpenApi();
    }
}