// DTOs/MovimientoDto.cs
using MicroservicioProductos.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

public class MovimientoDto
{
    [Required]
    public string NumeroCuenta { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Range(0.01, double.MaxValue)]
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