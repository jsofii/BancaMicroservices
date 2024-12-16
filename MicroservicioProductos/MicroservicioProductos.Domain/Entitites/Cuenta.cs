using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioClientes.Domain.Entities
{
    public class Cuenta
    {
        [Key]
        public int CuentaId { get; private set; }

        [Required]
        [MaxLength(20)]
        public string NumeroDeCuenta { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; private set; } // Ejemplo: "Ahorro", "Corriente"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoInicial { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Relación con Cliente
        [Required]
        public int ClienteId { get; private set; }

        // Relación con Movimiento
        public ICollection<Movimiento> Movimientos { get; private set; }

        // Constructor vacío para EF Core
        private Cuenta() { }

        // Constructor para creación
        public Cuenta(string numeroDeCuenta, string tipo, decimal saldoInicial, bool estado, int clienteId)
        {
            NumeroDeCuenta = numeroDeCuenta;
            Tipo = tipo;
            SaldoInicial = saldoInicial;
            Estado = estado;
            ClienteId = clienteId;
            Movimientos = new List<Movimiento>();
        }

        // Métodos para modificar el estado
        public void ActualizarSaldo(decimal nuevoSaldo)
        {
            SaldoInicial = nuevoSaldo;
        }

        public void CambiarEstado(bool nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}