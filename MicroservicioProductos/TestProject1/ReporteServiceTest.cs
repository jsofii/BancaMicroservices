using MicroservicioClientes.Domain;
using MicroservicioProductos.Application.Services;
using MicroservicioProductos.Infrastructure.Models;
using NSubstitute;
using TestProject1.Helpers;

namespace TestProject1
{
    public class ReporteServiceTest
    {
        private readonly IApplicationDbContext _context;
        private readonly ReportService _reportService;

        public ReporteServiceTest()
        {
            _context = Substitute.For<IApplicationDbContext>();
            _reportService = new ReportService(_context);
        }

        [Fact]
        public async Task GenerarEstadoDeCuentaReporteAsync_ShouldReturnReporte()
        {
            // Arrange
            var clienteId = 123;
            var fechaInicio = new DateTime(2023, 1, 1);
            var fechaFin = new DateTime(2023, 12, 31);

            var cuentas = new List<Cuenta>
            {
                new Cuenta { NumeroCuenta = "001", ClienteId = clienteId, SaldoInicial = 1000 }
            };

            var movimientos = new List<Movimiento>
            {
                new Movimiento { Id = 333, Fecha = new DateTime(2023, 6, 1), Monto = 200, Cuenta = cuentas.First() }
            };

            var mockCuentas = DbSetMock.CreateDbSetMock(cuentas);
            var mockMovimientos = DbSetMock.CreateDbSetMock(movimientos);

            _context.Cuentas.Returns(mockCuentas);
            _context.Movimientos.Returns(mockMovimientos);

            // Act
            var reporte = await _reportService.GenerarEstadoDeCuentaReporteAsync(clienteId, fechaInicio, fechaFin);

            // Assert
            Assert.NotNull(reporte);
            Assert.Equal(fechaInicio, reporte.FechaInicio);
            Assert.Equal(fechaFin, reporte.FechaFin);
            Assert.Single(reporte.Cuentas);
            Assert.Equal("001", reporte.Cuentas.First().CuentaId);
            Assert.Equal(1000, reporte.Cuentas.First().Saldo);
            Assert.Single(reporte.Cuentas.First().Movimientos);
            Assert.Equal(200, reporte.Cuentas.First().Movimientos.First().Monto);
        }
    }
}