using MicroservicioClientes.Domain;

public class ClienteDto
{
    public int Id { get; set; }
    public string ClienteId { get; set; }
    public string Nombre { get; set; }
    public bool Estado { get; set; }
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
            Estado = cliente.Estado
        };
    }

    public static Cliente ToEntity(this ClienteDto clienteDto)
    {
        return new Cliente
        {
            Id = clienteDto.Id,
            ClienteId = clienteDto.ClienteId,
            Nombre = clienteDto.Nombre,
            Estado = clienteDto.Estado
        };
    }
}