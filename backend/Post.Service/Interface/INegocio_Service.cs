using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Interface
{
    public interface INegocio_Service
    {
        Task<Negocio?> Get();
        Task<Negocio> Save(Negocio negocio);
    }
}
