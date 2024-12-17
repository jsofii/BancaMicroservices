// DTOs/CuentaDto.cs
using MicroservicioProductos.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

public class CuentaDto
{
    [Required]
    public string NumeroCuenta { get; set; }

    [Required]
    public string Cliente { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SaldoInicial { get; set; }

    public Cuenta ToEntity()
    {
        return new Cuenta
        {
            NumeroCuenta = this.NumeroCuenta,
            Cliente = this.Cliente,
            SaldoInicial = this.SaldoInicial
        };
    }

    public static CuentaDto ToDto(Cuenta cuenta)
    {
        return new CuentaDto
        {
            NumeroCuenta = cuenta.NumeroCuenta,
            Cliente = cuenta.Cliente,
            SaldoInicial = cuenta.SaldoInicial
        };
    }
}