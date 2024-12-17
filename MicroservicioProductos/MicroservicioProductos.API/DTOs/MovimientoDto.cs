// DTOs/MovimientoDto.cs
using MicroservicioProductos.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MovimientoDto
{
    [Required]
    public int CuentaId { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    [MaxLength(10)]
    public string TipoMovimiento { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Monto { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }

    public Movimiento ToEntity()
    {
        return new Movimiento
        {
            CuentaId = this.CuentaId,
            Fecha = this.Fecha,
            TipoMovimiento = this.TipoMovimiento,
            Monto = this.Monto,
            Saldo = this.Saldo
        };
    }

    public static MovimientoDto ToDto(Movimiento movimiento)
    {
        return new MovimientoDto
        {
            CuentaId = movimiento.CuentaId,
            Fecha = movimiento.Fecha,
            TipoMovimiento = movimiento.TipoMovimiento,
            Monto = movimiento.Monto,
            Saldo = movimiento.Saldo
        };
    }
}