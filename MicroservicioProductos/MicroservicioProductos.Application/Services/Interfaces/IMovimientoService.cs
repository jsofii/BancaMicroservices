using MicroservicioClientes.Domain.Entities;

namespace MicroservicioProductos.Application.Services.Interfaces;

public interface IMovimientoService
{
    public Task<Movimiento> RegistrarMovimientoAsync(Movimiento movimiento);
}