using FluentValidation;

namespace MicroservicioClientes.API.Validators;


public class ClienteDtoValidator : AbstractValidator<ClienteDto>
{
    public ClienteDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre es requerido.");
        RuleFor(x => x.Genero).NotEmpty().WithMessage("Genero is requerido.");

    }
}