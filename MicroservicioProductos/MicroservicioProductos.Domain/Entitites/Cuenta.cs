// MicroservicioProductos.Domain/Entities/Cuenta.cs
using System.ComponentModel.DataAnnotations;

namespace MicroservicioProductos.Infrastructure.Models
{
    public class Cuenta
    {
        [Key]
        [Required]
        [MaxLength(20)]
        public string NumeroCuenta { get; set; } // Número de cuenta

        [Required]
        [MaxLength(20)]
        public string TipoCuenta { get; set; } // Tipo de cuenta (Corriente, Ahorros, etc.)

        [Required]
        public decimal SaldoInicial { get; set; } // Saldo inicial de la cuenta

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Estado de la cuenta (Activa, Inactiva)

        [Required]
        public int Id { get; set; } // Identificador de la cuenta

        [Required]
        public int ClienteId { get; set; } // Identificador del cliente
    }
}