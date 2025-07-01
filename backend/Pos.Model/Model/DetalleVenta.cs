using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Model
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idDetalleVenta { get; set; }
        [ForeignKey(nameof(Venta))]
        public int idVenta { get; set; }
        public virtual Venta? Venta { get; set; }

        [ForeignKey(nameof(Producto))]
        public int idProducto { get; set; }
        public virtual Producto? Producto { get; set;}
        public string nombreProducto { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
    }
}
