using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class VentaDto
    {
        public int idVenta { get; set; }
        public string? factura { get; set; }
        public DateOnly fecha { get; set; }
        public string dni { get; set; } = string.Empty;
        public string cliente { get; set; } = string.Empty;
        public decimal descuento { get; set; }
        public decimal total { get; set; }

        public int idUsuario { get; set; }
        public EstadoVenta estado { get; set; } = EstadoVenta.Activa;
        public DateOnly? fechaAnulada { get; set; }
        public string? motivo { get; set; }
        public int? usuarioAnula { get; set; }
        public List<DetalleVentaDto>? DetalleVentas { get; set; }
    }
}
