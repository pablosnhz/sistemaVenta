using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class CategoriaDto
    {
        public int idCategoria { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
