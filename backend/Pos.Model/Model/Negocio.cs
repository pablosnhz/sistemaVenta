using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Model
{
    public class Negocio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idNegocio { get; set; }
        public string ruc { get; set; } = string.Empty;
        public string razonSocial { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string propietario { get; set; } = string.Empty;
        public decimal descuento { get; set; }
        public DateTime fechaRegistro { get; private set; }
    }
}
