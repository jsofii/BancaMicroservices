using MicroservicioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioClientes.Domain
{
   public interface IApplicationDbContext
   {
      DbSet<Cuenta> Cuentas { get; }
      DbSet<Movimiento> Movimientos { get; }
      Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
   }
}