using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Model
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idVenta { get; set; }
        public string? factura { get; set; }
        public DateOnly fecha { get; set; }
        public string dni { get; set; } = string.Empty;
        public string cliente { get; set; } = string.Empty;
        public decimal descuento { get; set; }
        public decimal total { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int idUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public EstadoVenta estado { get; set; } = EstadoVenta.Activa;
        public DateOnly? fechaAnulada { get; set; }
        public string? motivo { get; set; }
        public int? usuarioAnula { get; set; }

        public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
    }

    public enum EstadoVenta
    {
        Activa = 1,
        Anulada = 0
    }
}
