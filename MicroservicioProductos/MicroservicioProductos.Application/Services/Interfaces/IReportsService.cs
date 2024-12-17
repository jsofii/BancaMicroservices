using MicroservicioClientes.Domain.Entities;

namespace MicroservicioProductos.Application.Services.Interfaces;

public interface IReportsService
{
    Task<EstadoDeCuentaReporte> GenerarEstadoDeCuentaReporteAsync(string clienteId, 
        DateTime fechaInicio, DateTime fechaFin);

}