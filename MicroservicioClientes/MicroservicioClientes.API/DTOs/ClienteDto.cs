using System.ComponentModel.DataAnnotations;
using MicroservicioClientes.Domain;

public class ClienteDto
{
    public int Id { get; set; }

    [Required]
    public string ClienteId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    public bool Estado { get; set; }

    [Required]
    public string? Genero { get; set; }
}

public static class ClienteMapper
{
    public static ClienteDto ToDto(this Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            ClienteId = cliente.ClienteId,
            Nombre = cliente.Nombre,
            Estado = cliente.Estado,
            Genero = cliente.Genero
        };
    }

    public static Cliente ToEntity(this ClienteDto clienteDto)
    {
        return new Cliente
        {
            Id = clienteDto.Id,
            ClienteId = clienteDto.ClienteId,
            Nombre = clienteDto.Nombre,
            Estado = clienteDto.Estado,
            Genero = clienteDto.Genero
        };
    }
}