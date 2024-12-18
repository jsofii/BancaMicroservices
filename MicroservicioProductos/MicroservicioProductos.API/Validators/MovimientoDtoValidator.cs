using FluentValidation;

namespace MicroservicioProductos.Application.Validators
{
    public class MovimientoDtoValidator : AbstractValidator<MovimientoDto>
    {
        public MovimientoDtoValidator()
        {
            RuleFor(x => x.CuentaId).GreaterThan(0).WithMessage("Cuenta Id no puede ser 0.");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto debe ser mayor a 0");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("Fecha es requerida");
        }
    }
}