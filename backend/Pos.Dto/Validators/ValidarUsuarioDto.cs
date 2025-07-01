using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarUsuarioDto : AbstractValidator<UsuarioDto>
    {
        public ValidarUsuarioDto()
        {
            RuleFor(u => u.nombres)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del usuario.")
                .MaximumLength(35).WithMessage("El nombre del usuario no debe superar los 35 caracteres.");

            RuleFor(u => u.apellidos)
                .NotEmpty().WithMessage("Es necesario especificar el apellido del usuario.")
                .MaximumLength(35).WithMessage("El apellido del usuario no debe superar los 35 caracteres.");

            RuleFor(u => u.idRol)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol valido.");

            RuleFor(u => u.Telefono)
                .NotEmpty().WithMessage("Es necesario especificar el numero de telefono.")
                .MaximumLength(8).WithMessage("El numero de telefono debe contener al menos 8 caracteres")
                .MaximumLength(15).WithMessage("El numero de telefono no debe superar los 15 caracteres");

            RuleFor(u => u.email)
                .NotEmpty().WithMessage("Es necesario especificar el email.")
                .MaximumLength(50).WithMessage("El email no debe superar los 50 caracteres.");

            RuleFor(u => u.clave)
                .NotEmpty().WithMessage("Es necesario especificar una clave del usuario.")
                .MinimumLength(8).WithMessage("La clave debe tener al menos 8 caracteres");

            RuleFor(u => u.estado)
                .NotEmpty().WithMessage("Se solicita especificar el estado del usuario.")
                .MaximumLength(50).WithMessage("El estado del usuario no debe superar los 8 caracteres.")
                .Must(estado => estado == "Activo" || estado == "Inactivo").WithMessage("El estado debe ser 'activo' o 'inactivo'.");

        }
    }
}
