using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarProductoDto : AbstractValidator<ProductoDto>
    {
        public ValidarProductoDto() {
            RuleFor(p => p.codigoBarra)
                .NotEmpty().WithMessage("Es necesario especificar el codigo de barra.")
                .MaximumLength(30).WithMessage("El codigo de barra no debe de superar los 30 caracteres.");

            RuleFor(p => p.descripcion)
                .NotEmpty().WithMessage("Debe especificar la descripcion del producto.")
                .MaximumLength(50).WithMessage("El nombre del producto no debe de superar los 50 caracteres.");

            RuleFor(p => p.idCategoria)
                .GreaterThan(0).WithMessage("Debe de seleccionar una categoria valida.");

            RuleFor(p => p.precioVenta)
                .GreaterThanOrEqualTo(0).WithMessage("El precio de venta debe de ser mayor a 0.");

            RuleFor(p => p.stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser menor a 0.");

            RuleFor(p => p.stockMinimo)
                .GreaterThan(0).WithMessage("El stock minimo debe ser mayor a 0.");

            RuleFor(r => r.estado)
                .NotEmpty().WithMessage("Se solicita especificar el estado del producto.")
                .MaximumLength(50).WithMessage("El estado del producto no debe superar los 8 caracteres.")
                .Must(estado => estado == "Activo" || estado == "Inactivo").WithMessage("El estado debe ser 'activo' o 'inactivo'.");

        }
    }
}
