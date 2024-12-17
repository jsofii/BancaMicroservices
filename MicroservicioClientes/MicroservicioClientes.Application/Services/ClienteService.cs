using MicroservicioClientes.Domain;
using Microsoft.EntityFrameworkCore;
namespace MicroservicioClientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IApplicationDbContext _context;

        public ClienteService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            var existingCliente = await _context.Clientes.FindAsync(cliente.Id);
            if (existingCliente == null) return null;

            existingCliente.Nombre = cliente.Nombre;

            await _context.SaveChangesAsync();
            return existingCliente;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public object? ModelState { get; set; }
    }
}