// MicroservicioProductos.Infrastructure/Persistence/ApplicationDbContext.cs
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
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.NumeroCuenta)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.TipoCuenta)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.SaldoInicial)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Movimiento>()
                .ToTable("Movimientos")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Fecha)
                .IsRequired();

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.TipoMovimiento)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Monto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Saldo)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}