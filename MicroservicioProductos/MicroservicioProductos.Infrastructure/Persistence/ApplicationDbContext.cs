
using MicroservicioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioProductos.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<Cuenta>()
            .HasIndex(c => c.CuentaId)
            .IsUnique();

        modelBuilder.Entity<Movimiento>()
            .HasIndex(m => m.CuentaId)
            .IsUnique();

        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Cuenta)
            .WithMany()
            .HasForeignKey(m => m.CuentaId);
    }
}