using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    public class ValidarDetalleVentaDto : AbstractValidator<DetalleVentaDto>
    {
        public ValidarDetalleVentaDto()
        {
            RuleFor(dv => dv.idVenta)
                .GreaterThan(0).WithMessage("No se ha especificado el ID de la venta.");

            RuleFor(dv => dv.idProducto)
                .GreaterThan(0).WithMessage("No se ha especificado el ID del producto.");

            RuleFor(dv => dv.nombreProducto)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del producto.")
                .MaximumLength(50).WithMessage("El nombre del producto no debe de superar los 50 caracteres.");

            RuleFor(dv => dv.precio)
                .NotEmpty().WithMessage("Es necesario especificar el precio del producto")
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(dv => dv.cantidad)
                .NotEmpty().WithMessage("Es necesario especificar la cantidad de venta.")
                .GreaterThan(0).WithMessage("La cantidad de venta debe ser mayor a cero.");

            RuleFor(dv => dv.descuento)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser menor a cero.");

            RuleFor(dv => dv.total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero.");

        }
    }
}
