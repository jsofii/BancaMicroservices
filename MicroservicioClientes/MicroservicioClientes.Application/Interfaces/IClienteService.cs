using MicroservicioClientes.Domain;
namespace MicroservicioClientes.Application.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(Guid id);
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<Cliente?> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(Guid id);
        object? ModelState { get; set; }
    }
}