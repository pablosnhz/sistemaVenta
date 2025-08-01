using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Vistas
{
    public class ProductosMasVendidosView
    {
        public int idProducto { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int CantidadVendida { get; set; }
    }
}
