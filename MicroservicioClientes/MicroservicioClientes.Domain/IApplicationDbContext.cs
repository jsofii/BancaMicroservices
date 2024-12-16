using Microsoft.EntityFrameworkCore;

namespace MicroservicioClientes.Domain
{
    public interface IApplicationDbContext
    {
        DbSet<Cliente> Clientes { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}