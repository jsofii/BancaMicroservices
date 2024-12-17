using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Application.Services.Interfaces;

using MicroservicioClientes.Domain;
using MicroservicioProductos.Application.Exceptions;
using MicroservicioProductos.Infrastructure.Models;

namespace MicroservicioProductos.Application.Services;

public class MovimientoService : IMovimientoService
{
    private readonly IApplicationDbContext _context;

    public MovimientoService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Movimiento> RegistrarMovimientoAsync(Movimiento movimiento)
    {
        var cuenta = await _context.Cuentas.FindAsync(movimiento.NumeroCuenta);
        if (cuenta == null)
        {
            throw new KeyNotFoundException("Cuenta no encontrada");
        }
        // Check por el balance
        if (cuenta.SaldoInicial + movimiento.Monto < 0)
        {
            throw new InsufficientBalanceException("Saldo no disponible");
        }

        // Actualizar el saldo de la cuenta
        cuenta.SaldoInicial += movimiento.Monto;

        // Registrar el movimiento
        _context.Movimientos.Add(movimiento);
        await _context.SaveChangesAsync();

        return movimiento;
    }
}