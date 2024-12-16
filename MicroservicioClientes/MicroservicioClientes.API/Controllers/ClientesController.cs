using Microsoft.AspNetCore.Mvc;
using MicroservicioClientes.Application.Services;
using MicroservicioClientes.Domain;

namespace MicroservicioClientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _clienteService.CreateAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = created.ClienteId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ClienteId) return BadRequest();
            var updated = await _clienteService.UpdateAsync(cliente);
            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _clienteService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}