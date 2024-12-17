using MicroservicioClientes.Domain.Entities;
using MicroservicioProductos.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioProductos.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cuenta>()
                .ToTable("Cuentas")
                .HasKey(c => c.NumeroCuenta);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.Cliente)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.SaldoInicial)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.Cliente)
                .IsRequired();

            modelBuilder.Entity<Movimiento>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Monto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Fecha)
                .IsRequired();

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.TipoMovimiento)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.NumeroCuenta)
                .IsRequired()
                .HasMaxLength(20);


        }
    }
}