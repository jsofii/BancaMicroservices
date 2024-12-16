namespace MicroservicioClientes.Domain.Entities;

public class EstadoDeCuentaReporte
{
    public string ClienteId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public List<CuentaReporte> Cuentas { get; set; }
}

public class CuentaReporte
{
    public int CuentaId { get; set; }
    public decimal Saldo { get; set; }
    public List<Movimiento> Movimientos { get; set; }
}