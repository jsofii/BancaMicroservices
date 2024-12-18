namespace MicroservicioProductos.Application.Exceptions;

public class NoAccountAssociatedFoundException: Exception
{
    public NoAccountAssociatedFoundException(string message) : base(message)
    {
    }
}