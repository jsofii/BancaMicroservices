using FluentValidation;
using FluentValidation.Results;
using MicroservicioClientes.Application.Services;
using MicroservicioClientes.Domain;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder endpoints)
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

        group.MapPost("/", async (ClienteDto clienteDto, IValidator<ClienteDto> validator, IClienteService clienteService) =>
        {
            ValidationResult result = await validator.ValidateAsync(clienteDto);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors);
            }

            var created = await clienteService.CreateAsync(clienteDto.ToEntity());
            return Results.Created($"/api/clientes/{created.ClienteId}", created);
        });

        group.MapPut("/{id:int}", async (int id, ClienteDto clienteDto, IValidator<ClienteDto> validator, IClienteService clienteService) =>
        {
            clienteDto.Id = id;
            ValidationResult result = await validator.ValidateAsync(clienteDto);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors);
            }

            var updated = await clienteService.UpdateAsync(clienteDto.ToEntity());
            return updated is not null ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IClienteService clienteService) =>
        {
            var deleted = await clienteService.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}