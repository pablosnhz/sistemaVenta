using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class ProductosMasVendidosDto
    {
        public int idProducto { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int CantidadVendida { get; set; }
    }
}
