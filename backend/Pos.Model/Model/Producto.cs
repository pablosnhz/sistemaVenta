using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Model
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idProducto { get; set; }
        public string codigoBarra { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;

        [ForeignKey(nameof(Categoria))]
        public int idCategoria { get; set; }
        public virtual Categoria? categoria { get; set; }
        public decimal precioVenta { get; set; }
        public int stock { get; set; }
        public int stockMinimo { get; set; }
        public string estado { get; set; } = string.Empty;
        public DateTime fechaRegistro { get; private set; }

    }
}
