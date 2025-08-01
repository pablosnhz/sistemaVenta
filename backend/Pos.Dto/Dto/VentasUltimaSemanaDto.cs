using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Dto.Dto
{
    public class VentasUltimaSemanaDto
    {
        public DateOnly Fecha { get; set; }
        public int TotalVentas { get; set; }
    }
}
