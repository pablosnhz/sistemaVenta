using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Repository.Interface
{
    public interface INegocio_Repository
    {
        Task<Negocio?> Get();
        Task<Negocio?> Create(Negocio negocio);
        Task<Negocio?> Update(Negocio negocio);
    }
}
