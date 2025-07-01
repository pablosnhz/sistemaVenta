using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarNegocioDto : AbstractValidator<NegocioDto>
    {
        public ValidarNegocioDto()
        {
            RuleFor(n => n.ruc)
                .NotEmpty().WithMessage("Es necesario especificar el numero ruc de la empresa")
                .MaximumLength(20).WithMessage("El numero ruc no debe superar los 20 caracteres.");

            RuleFor(n => n.razonSocial)
                .NotEmpty().WithMessage("Es necesario especificar el nombre de la empresa")
                .MaximumLength(50).WithMessage("El numero ruc no debe superar los 50 caracteres.");

            RuleFor(n => n.email)
                .NotEmpty().WithMessage("Es necesario especificar el email de la empresa")
                .MaximumLength(50).WithMessage("El email no debe superar los 50 caracteres.");

            RuleFor(n => n.telefono)
                .NotEmpty().WithMessage("Es necesario especificar el numero de telefono.")
                .MaximumLength(8).WithMessage("El numero de telefono debe contener al menos 8 caracteres")
                .MaximumLength(15).WithMessage("El numero de telefono no debe superar los 15 caracteres");

            RuleFor(n => n.direccion)
                .NotEmpty().WithMessage("Es necesario especificar la direccion de la empresa.")
                .MaximumLength(500).WithMessage("La direccion no debe superar los 500 caracteres");

            RuleFor(n => n.propietario)
                .NotEmpty().WithMessage("Es necesario especificar el numero del propietario de la empresa.")
                .MaximumLength(3).WithMessage("El nombre del propietario debe contener al menos 3 caracteres")
                .MaximumLength(50).WithMessage("El nombre del propietario no debe superar los 50 caracteres");

            RuleFor(n => n.descuento)
                .NotEmpty().WithMessage("Es necesario especificar el descuento.")
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser menor a 0.");
        }
    }
}
