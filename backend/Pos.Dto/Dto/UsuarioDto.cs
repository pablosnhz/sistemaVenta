using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class UsuarioDto
    {
        public int idUsuario { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;

        public int idRol { get; set; }
        public string rolDescripcion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string clave { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
