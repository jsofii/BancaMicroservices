// DTOs/MovimientoDto.cs
using MicroservicioProductos.Infrastructure.Models;

public class MovimientoDto
{
    public string NumeroCuenta { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }

    public Movimiento ToEntity()
    {
        return new Movimiento
        {
            NumeroCuenta = this.NumeroCuenta,
            Fecha = this.Fecha,
            Monto = this.Monto
        };
    }

    public static MovimientoDto ToDto(Movimiento movimiento)
    {
        return new MovimientoDto
        {
            NumeroCuenta = movimiento.NumeroCuenta,
            Fecha = movimiento.Fecha,
            Monto = movimiento.Monto
        };
    }
}