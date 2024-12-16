using MicroservicioClientes.Domain;
using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioProductos.Application.Services;

public class ReportService: IReportsService
{
    private readonly IApplicationDbContext _context;

    public ReportService(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<EstadoDeCuentaReporte> GenerarEstadoDeCuentaReporteAsync(
        int clienteIdString, DateTime fechaInicio, DateTime fechaFin)
    {

        // Obtener las cuentas asociadas al cliente
        var cuentas = await _context.Cuentas
            .Where(c => c.ClienteId == clienteIdString)
            .ToListAsync();

        // Obtener los movimientos de las cuentas dentro del rango de fechas
        var movimientos = await _context.Movimientos
            .Where(m => m.Cuenta.ClienteId == clienteIdString 
                        && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
            .ToListAsync();

        // Crear el reporte
        var reporte = new EstadoDeCuentaReporte
        {
            ClienteId = clienteIdString.ToString(),
            FechaInicio = fechaInicio,
            FechaFin = fechaFin,
            Cuentas = cuentas.Select(c => new CuentaReporte
            {
                CuentaId = c.CuentaId,
                Saldo = c.SaldoInicial,
                Movimientos = movimientos.Where(m => m.CuentaId == c.CuentaId).ToList()            }).ToList()
        };

        return reporte;
    }

    
}