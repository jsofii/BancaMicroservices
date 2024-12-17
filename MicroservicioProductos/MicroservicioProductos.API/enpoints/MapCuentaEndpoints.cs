// CuentaEndpoints.cs

using MicroservicioProductos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class CuentaEndpoints
{
    public static void MapCuentaEndpoints(this IEndpointRouteBuilder endpoints)
{
    endpoints.MapPost("/cuentas", async (ApplicationDbContext context, CuentaDto cuentaDto) =>
    {
        var cuenta = cuentaDto.ToEntity();
        context.Cuentas.Add(cuenta);
        await context.SaveChangesAsync();
        return Results.Created($"/cuentas/{cuenta.NumeroCuenta}", cuentaDto);
    })
    .WithName("CreateCuenta")
    .WithOpenApi();

    endpoints.MapGet("/cuentas", async (ApplicationDbContext context) =>
    {
        var cuentas = await context.Cuentas.ToListAsync();
        var cuentaDtos = cuentas.Select(CuentaDto.ToDto).ToList();
        return Results.Ok(cuentaDtos);
    })
    .WithName("GetCuentas")
    .WithOpenApi();

    endpoints.MapGet("/cuentas/{id}", async (ApplicationDbContext context, int id) =>
    {
        var cuenta = await context.Cuentas.FindAsync(id);
        if (cuenta is null) return Results.NotFound();
        var cuentaDto = CuentaDto.ToDto(cuenta);
        return Results.Ok(cuentaDto);
    })
    .WithName("GetCuentaById")
    .WithOpenApi();

    endpoints.MapPut("/cuentas/{id}", async (ApplicationDbContext context, int id, CuentaDto updatedCuentaDto) =>
    {
        var cuenta = await context.Cuentas.FindAsync(id);
        if (cuenta is null) return Results.NotFound();
        cuenta.NumeroCuenta = updatedCuentaDto.NumeroCuenta;
        cuenta.SaldoInicial = updatedCuentaDto.SaldoInicial;
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