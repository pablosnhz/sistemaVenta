using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class NegocioDto
    {
        public int idNegocio { get; set; }
        public string ruc { get; set; } = string.Empty;
        public string razonSocial { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string propietario { get; set; } = string.Empty;
        public decimal descuento { get; set; }
    }
}
