using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Application.Services.Interfaces;

using MicroservicioClientes.Domain;
using MicroservicioProductos.Application.Exceptions;

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
        var cuenta = await _context.Cuentas.FindAsync(movimiento.CuentaId);
        if (cuenta == null)
        {
            throw new KeyNotFoundException("Cuenta no encontrada");
        }
        // Check if the balance is sufficient
        if (cuenta.SaldoInicial + movimiento.Valor < 0)
        {
            throw new InsufficientBalanceException("Saldo no disponible");
        }

        // Actualizar el saldo de la cuenta
        cuenta.SaldoInicial += movimiento.Valor;

        // Registrar el movimiento
        _context.Movimientos.Add(movimiento);
        await _context.SaveChangesAsync();

        return movimiento;
    }
}