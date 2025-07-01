using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarRolDto : AbstractValidator<RolDto>
    {
        public ValidarRolDto() {
            RuleFor(r => r.descripcion)
                .NotEmpty().WithMessage("Se solicita especificar la descripcion del rol.")
                .MaximumLength(50).WithMessage("La descripcion del rol no debe superar los 50 caracteres.");

            RuleFor(r => r.estado)
                .NotEmpty().WithMessage("Se solicita especificar el estado del rol.")
                .MaximumLength(50).WithMessage("El estado del rol no debe superar los 8 caracteres.")
                .Must(estado => estado == "Activo" || estado == "Inactivo").WithMessage("El estado debe ser 'activo' o 'inactivo'.");

        }
    }
}
