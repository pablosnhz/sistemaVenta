using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class DetalleVentaDto
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
       
        public int idProducto { get; set; }
        public string nombreProducto { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
    }
}
