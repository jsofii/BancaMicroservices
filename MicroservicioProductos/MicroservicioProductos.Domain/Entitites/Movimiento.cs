using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioProductos.Infrastructure.Models
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; } // Identificador único del movimiento

        [Required]
        [MaxLength(20)]
        public string NumeroCuenta { get; set; } // Relacionado con el número de cuenta

        [Required]
        [MaxLength(50)]
        public string TipoMovimiento { get; set; } // Tipo: "Retiro" o "Depósito"

        [Required]
        public decimal Monto { get; set; } // Monto del movimiento

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now; // Fecha del movimiento

        // Relación con Cuenta
        [ForeignKey("NumeroCuenta")]
        public Cuenta Cuenta { get; set; } // Propiedad de navegación
    }
}