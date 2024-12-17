// DTOs/CuentaDto.cs
using MicroservicioProductos.Infrastructure.Models;

public class CuentaDto
{
    public string NumeroCuenta { get; set; }
    public string Cliente { get; set; }
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