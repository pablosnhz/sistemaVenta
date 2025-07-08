using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Repository.Interface
{
    public interface IDocumento_Repository
    {
        Task<NumeroDocumento?> Get();
        Task<NumeroDocumento> Create(NumeroDocumento documento);
        Task<NumeroDocumento> Update(NumeroDocumento documento);

    }
}
