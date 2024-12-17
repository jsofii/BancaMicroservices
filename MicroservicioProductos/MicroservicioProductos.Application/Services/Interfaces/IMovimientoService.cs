using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Infrastructure.Models;

namespace MicroservicioProductos.Application.Services.Interfaces;

public interface IMovimientoService
{
    public Task<Movimiento> RegistrarMovimientoAsync(Movimiento movimiento);
}