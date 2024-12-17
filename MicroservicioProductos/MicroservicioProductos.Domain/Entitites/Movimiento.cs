// MicroservicioProductos.Infrastructure/Models/Movimiento.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioProductos.Infrastructure.Models
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(10)]
        public string TipoMovimiento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        [Required]
        public int CuentaId { get; set; } // Foreign key to Cuenta

        [ForeignKey("CuentaId")]
        public Cuenta Cuenta { get; set; }
    }
}