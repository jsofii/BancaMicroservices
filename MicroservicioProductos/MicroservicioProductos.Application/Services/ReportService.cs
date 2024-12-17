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
        string clienteId, DateTime fechaInicio, DateTime fechaFin)
    {

        // Obtener las cuentas asociadas al cliente
        var cuentas = await _context.Cuentas
            .Where(c => c.Cliente == clienteId)
            .ToListAsync();

        // Obtener los movimientos de las cuentas dentro del rango de fechas
        var movimientos = await _context.Movimientos
            .Where(m => m.Cuenta.Cliente == clienteId 
                        && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
            .ToListAsync();

        // Crear el reporte
        var reporte = new EstadoDeCuentaReporte
        {
            ClienteId = clienteId.ToString(),
            FechaInicio = fechaInicio,
            FechaFin = fechaFin,
            Cuentas = cuentas.Select(c => new CuentaReporte
            {
                CuentaId = c.NumeroCuenta,
                Saldo = c.SaldoInicial,
                Movimientos = movimientos.Where(m => m.NumeroCuenta == c.NumeroCuenta).ToList()}).ToList()
        };

        return reporte;
    }

    
}