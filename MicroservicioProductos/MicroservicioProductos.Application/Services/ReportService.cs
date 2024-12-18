using MicroservicioClientes.Domain;
using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Application.Exceptions;
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
        int clienteId, DateTime fechaInicio, DateTime fechaFin)
    {
        try
        {
            // Obtener las cuentas asociadas al cliente
            var cuentas = await _context.Cuentas
                .Where(c => c.ClienteId == clienteId)
                .ToListAsync();

            if (!cuentas.Any())
            {
                throw new NoAccountAssociatedFoundException($"No accounts found for client ID {clienteId}");
            }

            // Obtener los movimientos de las cuentas dentro del rango de fechas
            var movimientos = await _context.Movimientos
                .Where(m => m.Cuenta.ClienteId == clienteId
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
                    Movimientos = movimientos.Where(m => m.CuentaId == c.Id).ToList()
                }).ToList()
            };

            return reporte;
        }
        catch (NoAccountAssociatedFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            // Log the exception (logging mechanism not shown here)
            throw new ApplicationException("An error occurred while generating the account statement report.", ex);
        }
    }

    
}