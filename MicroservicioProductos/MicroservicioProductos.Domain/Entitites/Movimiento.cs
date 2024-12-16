using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioClientes.Domain.Entities
{
    public class Movimiento
    {
        [Key]
        public Guid MovimientoId { get; private set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoMovimiento { get; private set; } // Ejemplo: "Depósito", "Retiro".

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        // Relación con Cuenta
        [Required]
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; private set; }

        // Constructor vacío para EF Core
        private Movimiento() { }

        // Constructor para creación
        public Movimiento(int cuentaId, string tipoMovimiento, decimal valor, decimal saldo)
        {
            MovimientoId = Guid.NewGuid();
            Fecha = DateTime.UtcNow;
            TipoMovimiento = tipoMovimiento;
            Valor = valor;
            Saldo = saldo;
            CuentaId = cuentaId;
        }

        // Método para actualizar saldo
        public void ActualizarSaldo(decimal nuevoSaldo)
        {
            Saldo = nuevoSaldo;
        }
    }
}