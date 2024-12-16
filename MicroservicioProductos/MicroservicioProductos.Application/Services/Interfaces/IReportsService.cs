using MicroservicioClientes.Domain.Entities;

namespace MicroservicioProductos.Application.Services.Interfaces;

public interface IReportsService
{
    Task<EstadoDeCuentaReporte> GenerarEstadoDeCuentaReporteAsync(int clienteId, 
        DateTime fechaInicio, DateTime fechaFin);

}