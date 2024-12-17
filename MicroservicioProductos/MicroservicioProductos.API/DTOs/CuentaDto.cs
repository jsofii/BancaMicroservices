// DTOs/CuentaDto.cs
using MicroservicioProductos.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

public class CuentaDto
{
    [Required]
    public string NumeroCuenta { get; set; }

    [Required]
    public string TipoCuenta { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SaldoInicial { get; set; }

    [Required]
    public string Estado { get; set; }

    [Required]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    public Cuenta ToEntity()
    {
        return new Cuenta
        {
            NumeroCuenta = this.NumeroCuenta,
            TipoCuenta = this.TipoCuenta,
            SaldoInicial = this.SaldoInicial,
            Estado = this.Estado,
            Id = this.Id,
            ClienteId = this.ClienteId
        };
    }

    public static CuentaDto ToDto(Cuenta cuenta)
    {
        return new CuentaDto
        {
            NumeroCuenta = cuenta.NumeroCuenta,
            TipoCuenta = cuenta.TipoCuenta,
            SaldoInicial = cuenta.SaldoInicial,
            Estado = cuenta.Estado,
            Id = cuenta.Id,
            ClienteId = cuenta.ClienteId
        };
    }
}