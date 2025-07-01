using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Model
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set;} = string.Empty;

        [ForeignKey(nameof(Rol))]
        public int idRol { get; set; }
        public virtual Rol? Rol { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public DateTime fechaRegistro { get; private set; }

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
