using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarCategoriaDto : AbstractValidator<CategoriaDto>
    {
        public ValidarCategoriaDto()
        {
            RuleFor(c => c.descripcion)
                .NotEmpty().WithMessage("Se solicita especificar la descripcion de la categoria.")
                .MaximumLength(50).WithMessage("La descripcion de la categoria. no debe superar los 50 caracteres.");

            RuleFor(c => c.estado)
                .NotEmpty().WithMessage("Se solicita especificar el estado de la categoria.")
                .MaximumLength(50).WithMessage("El estado de la categoria no debe superar los 8 caracteres.")
                .Must(estado => estado == "Activo" || estado == "Inactivo").WithMessage("El estado debe ser 'activo' o 'inactivo'.");

        }
    }
}
