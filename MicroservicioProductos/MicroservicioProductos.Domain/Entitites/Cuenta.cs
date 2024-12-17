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
        public string Tipo { get; set; } // Tipo de cuenta (Corriente, Ahorros, etc.)

        [Required]
        public decimal SaldoInicial { get; set; } // Saldo inicial de la cuenta

        [Required]
        public bool Estado { get; set; } // Estado de la cuenta (True: Activa, False: Inactiva)

        [Required]
        [MaxLength(100)]
        public string Cliente { get; set; } // Nombre del cliente titular
    }
}