
using MicroservicioClientes.Application.Services;
using MicroservicioClientes.Domain;

public static class MovimientoEndpoints
{
  
    public static void MapMovimientosEndpoints(this IEndpointRouteBuilder endpoints)
    {
            var group = endpoints.MapGroup("/api/clientes");

            group.MapGet("/", async (IClienteService clienteService) =>
            {
                var clientes = await clienteService.GetAllAsync();
                return Results.Ok(clientes);
            });

            group.MapGet("/{id:guid}", async (Guid id, IClienteService clienteService) =>
            {
                var cliente = await clienteService.GetByIdAsync(id);
                return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
            });

            group.MapPost("/", async (Cliente cliente, IClienteService clienteService) =>
            {
                
                if (string.IsNullOrEmpty(cliente.Genero))
                {
                    return Results.BadRequest("Genero is required.");
                }

                var created = await clienteService.CreateAsync(cliente);
                return Results.Created($"/api/clientes/{created.ClienteId}", created);
            });

            group.MapPut("/{id:int}", async (int id, Cliente cliente, IClienteService clienteService) =>
            {
                cliente.Id = id;
                var updated = await clienteService.UpdateAsync(cliente);
                return updated is not null ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IClienteService clienteService) =>
            {
                var deleted = await clienteService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
}
