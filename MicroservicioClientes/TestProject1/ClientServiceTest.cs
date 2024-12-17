using MicroservicioClientes.Application.Services;
using MicroservicioClientes.Domain;
using NSubstitute;

public class ClientServiceTest
{
    private readonly IApplicationDbContext _context;
    private readonly ClienteService _clienteService;

    public ClientServiceTest()
    {
        _context = Substitute.For<IApplicationDbContext>();
        _clienteService = new ClienteService(_context);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCliente_WhenClienteExists()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, Nombre = "John Doe" };
        var existingCliente = new Cliente { Id = 1, Nombre = "Jane Doe" };

        _context.Clientes.FindAsync(cliente.Id).Returns(existingCliente);

        // Act
        var result = await _clienteService.UpdateAsync(cliente);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cliente.Nombre, result.Nombre);
        await _context.Received(1).SaveChangesAsync();
    }
}