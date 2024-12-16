using MicroservicioClientes.Domain;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioClientes.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración adicional para Cliente
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.ClienteId)
            .IsUnique();

        // Configuración adicional para Persona
        modelBuilder.Entity<Persona>()
            .HasIndex(p => p.Identificacion)
            .IsUnique();
        
        
    }
}