using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Vistas
{
    public class ProductosPorAgotarView
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Producto { get; set;} = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
