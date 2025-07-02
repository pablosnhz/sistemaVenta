using FluentValidation;
using Pos.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Validators
{
    internal class ValidarVentaDto : AbstractValidator<VentaDto>
    {
        public ValidarVentaDto()
        {
            RuleFor(v => v.dni)
                .NotEmpty().WithMessage("Es necesario especificar el numero de identificacion del cliente.")
                .MaximumLength(20).WithMessage("El numero de identificacion no de superar los 20 caracteres.");

            RuleFor(v => v.cliente)
                .NotEmpty().WithMessage("Se debe especificar el nombre del cliente")
                .MinimumLength(3).WithMessage("El nombre del cliente debe tener por lo menos 3 caracteres")
                .MaximumLength(50).WithMessage("El nombre del cliente no debe de superar los 50 caracteres.");

            RuleFor(v => v.descuento)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento de la venta no puede ser mejor que cero.");

            RuleFor(v => v.total)
                .GreaterThan(0).WithMessage("El total de la venta debe ser mayor de cero.");

            RuleFor(v => v.idUsuario)
                .GreaterThan(0).WithMessage("No se ha especificado el usuario que realizo la venta.");

            RuleFor(v => v.estado)
                .IsInEnum().WithMessage("El estado de la venta no es valido, selecciona una opcion de la lista desplegable.");

            RuleFor(v => v.fechaAnulada)
                .Null().When(v => v.estado == Model.Model.EstadoVenta.Activa).WithMessage("La fecha de anulacion debe ser nula si el estado de la venta es: 'Activa'.")
                .NotEmpty().When(v => v.estado == Model.Model.EstadoVenta.Anulada).WithMessage("La fecha de anulacion es requerida si el estado de la venta es anulada.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).When(v => v.fechaAnulada.HasValue).WithMessage("La fecha de anulacion no puede ser mayor a la fecha actual.");

            RuleFor(v => v.motivo)
                .NotEmpty().When(v => v.estado == Model.Model.EstadoVenta.Anulada).WithMessage("El motivo de anulacion de la venta es requerido cuando la venta es: 'Anulada'");

            RuleFor(v => v.usuarioAnula)
                .NotEmpty().When(v => v.estado == Model.Model.EstadoVenta.Anulada).WithMessage("El usuario que anula la venta es requerido si la venta es: 'Anulada'.");
        }
    }
}
