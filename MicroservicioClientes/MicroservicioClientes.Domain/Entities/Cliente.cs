namespace MicroservicioClientes.Domain;

public class Cliente : Persona
{
    public string ClienteId { get; set; }
    public string Contrasena { get; set; }
    public bool Estado { get; set; }
}